using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Windows.Documents;
using DBI_ShuffleTool.Entity;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DBI_ShuffleTool.Model
{
    class ShuffleExamModel
    {
        public QuestionsBank qbQuestionsBank;//QBank from Creator

        public List<String> eiItemCodeList;//Include Code ExamItem

        public List<ExamItem> eiList;//Include Exam after create

        public List<String> eiListForDuplicate;

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

            for (int i = 0; i < qbQuestionsBank.QBank.Count; i++)
            {
                listQForShuffle.Add(new Question(qbQuestionsBank.QBank.ElementAt(i).QuestionId, 
                    CopyList<Candidate>(qbQuestionsBank.QBank.ElementAt(i).Candidates)));
            }
            
            for (int i = 0; i < numOfPage; i++)
            {
                eiList.Add(CreateAExamItem(listQForShuffle, eiItemCodeList.ElementAt(i)));
            }
        }

        private ExamItem CreateAExamItem(List<Question> listQForShuffle, String codeExam)
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
                ei = CreateAExamItem(listQForShuffle, codeExam);
            }
            return new ExamItem(codeExam, listCandidate);
        }

        //ResetQuestions number i
        private List<Candidate> ResetQuestion(int i)
        {
            List<Candidate> listQC = new List<Candidate>();
            foreach(Candidate candi in qbQuestionsBank.QBank.ElementAt(i).Candidates)
            {
                listQC.Add(candi);
            }
            return listQC;
        }

        //Copy List
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

        //Return examItemList
        public List<ExamItem> GetExamItemsList()
        {
            return eiList;
        }
        
        //Create CodeExamItem
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

        //Get a random number from min to max
        private int GetRandomNumber(int min, int max)
        {
            return new Random().Next(min, max);
        }


        //Get a random Candidate From a Question
        public Candidate GetRdQCandidateFromQuestion(List<Candidate> qcList)
        {
            return qcList.ElementAt(GetRandomNumber(0, qcList.Count));
        }


        //check is a QuestionsList duplicated
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
