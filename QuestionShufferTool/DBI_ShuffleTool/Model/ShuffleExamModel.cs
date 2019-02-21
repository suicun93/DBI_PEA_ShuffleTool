using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using DBI_ShuffleTool.Entity;

namespace DBI_ShuffleTool.Model
{
    class ShuffleExamModel
    {
        public QuestionsBank qbQuestionsBank;//QBank from Creator

        public List<String> eiItemCodeList;//Include Code ExamItem

        public List<ExamItem> eiList;//Include Exam after create

        public List<String> eiListForDuplicate;//Include ExamCode for checking Duplicated
        /// <summary>
        /// Create List of ExamItem
        /// </summary>
        /// <param name="qbQuestionsBank"></param>
        /// <param name="numOfPage"></param>
        public ShuffleExamModel(QuestionsBank qbQuestionsBank, int numOfPage)
        {
            this.qbQuestionsBank = qbQuestionsBank;
            eiItemCodeList = new List<string>();
            eiList = new List<ExamItem>();
            eiListForDuplicate = new List<String>();
            for (int i = 0; i < numOfPage; i++)//Create Code of the ExamItem
            {
                eiItemCodeList.Add(GetRdCodeForExam());
            }

            List<Question> listQForShuffle = new List<Question>();
            //Create new ListQuestions from Questions Bank
            for (int i = 0; i < qbQuestionsBank.QBank.Count; i++)
            {
                listQForShuffle.Add(new Question(qbQuestionsBank.QBank.ElementAt(i).QuestionId, 
                    CopyList<Candidate>(qbQuestionsBank.QBank.ElementAt(i).Candidates)));
            }
            //Adding List of Questions into each ExamItem
            for (int i = 0; i < numOfPage; i++)
            {
                eiList.Add(CreateExamItem(listQForShuffle, eiItemCodeList.ElementAt(i)));
            }
        }

        

        /// <summary>
        /// Create an ExamItem from: list questions and code exam
        /// </summary>
        /// <param name="listQForShuffle"></param>
        /// <param name="codeExam"></param>
        /// <returns></returns>
        private ExamItem CreateExamItem(List<Question> listQForShuffle, String codeExam)
        {
            List<Candidate> listCandidate = new List<Candidate>();
            for (int i = 0; i < qbQuestionsBank.QBank.Count; i++)
            {
                Candidate candi = GetRdQCandidateFromQuestion(listQForShuffle.ElementAt(i).Candidates);
                listCandidate.Add(candi);
                listQForShuffle.ElementAt(i).Candidates.Remove(candi);
                if (listQForShuffle.ElementAt(i).Candidates.Count == 0)
                {
                    listQForShuffle.ElementAt(i).Candidates = ResetQuestion(i);
                }
            }
            ExamItem ei = new ExamItem(codeExam, listCandidate);
            if (IsDuplicated(ei))
            {
                ei = CreateExamItem(listQForShuffle, codeExam);
            }
            return new ExamItem(codeExam, listCandidate);
        }

        /// <summary>
        /// Reset after removing all elements in List of Candidates
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        private List<Candidate> ResetQuestion(int i)
        {
            List<Candidate> listQC = new List<Candidate>();
            foreach(Candidate candi in qbQuestionsBank.QBank.ElementAt(i).Candidates)
            {
                listQC.Add(candi);
            }
            return listQC;
        }

        /// <summary>
        /// Copy a List
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lst"></param>
        /// <returns>NewList</returns>
        public List<T> CopyList<T>(List<T> lst)
        {
            List<T> lstCopy = new List<T>();
            foreach (var item in lst)
            {
                using (MemoryStream stream = new MemoryStream())
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(stream, item);
                    stream.Position = 0;
                    lstCopy.Add((T)formatter.Deserialize(stream));
                }
            }
            return lstCopy;
        }

        /// <summary>
        /// Return ExamItemsList after creating
        /// </summary>
        /// <returns>List<ExamItem> eiList</ExamItem></returns>
        public List<ExamItem> GetExamItemsList()
        {
            return eiList;
        }
        
        /// <summary>
        /// Create a code exam
        /// </summary>
        /// <returns>a code of Exam</returns>
        public String GetRdCodeForExam()
        {
            String res;
            //Random a code which is not duplicated in examItemCodeList
            do
            {
                res = GetRandomNumber(100000, 999999).ToString();
            } while (eiItemCodeList.Contains(res));
            return res;
        }

        /// <summary>
        /// Random a number from min to max
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        private int GetRandomNumber(int min, int max)
        {
            return new Random().Next(min, max);
        }


        /// <summary>
        /// Get random an element in List
        /// </summary>
        /// <param name="qcList"></param>
        /// <returns></returns>
        public Candidate GetRdQCandidateFromQuestion(List<Candidate> qcList)
        {
            return qcList.ElementAt(GetRandomNumber(0, qcList.Count));
        }


        /// <summary>
        /// checking duplicated
        /// </summary>
        /// <param name="ei"></param>
        /// <returns></returns>
        private bool IsDuplicated(ExamItem ei)
        {
            String res = "";
            foreach(Candidate q in ei.ExamQuestionsList)
            {
                res = res + q.QuestionId + q.CandidateId;
            }
            if (eiListForDuplicate.Contains(res))
            {
                return true;
            }
            else
            {
                eiListForDuplicate.Add(res);
                return false;
            }
        }
    }
}
