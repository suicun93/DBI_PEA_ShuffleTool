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
        public List<Question> QbQuestionsBank;//QBank from Creator

        public List<String> EiItemCodeList;//Include Code ExamItem

        public List<ExamForDoc> EiListDoc;//Include Exam after create

        public List<ExamForMarking> EiListMarking;

        public List<String> EiListForDuplicate;//Include ExamCode for checking Duplicated
        /// <summary>
        /// Create List of ExamItem
        /// </summary>
        /// <param name="qbQuestionsBank"></param>
        /// <param name="numOfPage"></param>
        public ShuffleExamModel(List<Question> qbQuestionsBank, int numOfPage)
        {
            QbQuestionsBank = qbQuestionsBank;
            EiItemCodeList = new List<string>();
            EiListDoc = new List<ExamForDoc>();
            EiListForDuplicate = new List<string>();
            EiListMarking = new List<ExamForMarking>();
            for (int i = 0; i < numOfPage; i++)//Create Code of the ExamItem
            {
                EiItemCodeList.Add((i+1).ToString());
            }

            List<Question> listQForShuffle = new List<Question>();
            //Create new ListQuestions from Questions Bank
            foreach (Question q in qbQuestionsBank)
            {
                Question aQuestion = new Question();
                aQuestion.Point = q.Point;
                aQuestion.QuestionId = q.QuestionId;
                aQuestion.Candidates = CopyList(q.Candidates);
                listQForShuffle.Add(aQuestion);////Xem lai o day
            }
            //Adding List of Questions into each ExamItem
            for (int i = 0; i < numOfPage; i++)
            {
                EiListDoc.Add(CreateExamItem(listQForShuffle, EiItemCodeList.ElementAt(i)));
            }

            //Create List Exam for Marking
            foreach (ExamForDoc eDoc in EiListDoc)
            {
                ExamForMarking eMark = new ExamForMarking();
                List<CandidateExam> candidateExams = new List<CandidateExam>();
                foreach (Candidate candi in eDoc.ExamQuestionsList)
                {
                    CandidateExam candiExam = new CandidateExam();
                    candiExam.ActivateQuery = candi.ActivateQuery;
                    candiExam.CandidateId = candi.CandidateId;
                    candiExam.QuestionId = candi.QuestionId;
                    candiExam.QuestionType = candi.QuestionType;
                    candiExam.Requirements = candi.Requirements;
                    candiExam.Solution = candi.Solution;
                    candiExam.Point = candi.Point;
                    candidateExams.Add(candiExam);
                }
                eMark.ExamQuestionsList = candidateExams;
                eMark.PaperNo = eDoc.PaperNo;
                EiListMarking.Add(eMark);
            }
        }

        

        /// <summary>
        /// Create an ExamItem from: list questions and code exam
        /// </summary>
        /// <param name="listQForShuffle"></param>
        /// <param name="codeExam"></param>
        /// <returns></returns>
        private ExamForDoc CreateExamItem(List<Question> listQForShuffle, String codeExam)
        {
            List<Candidate> listCandidate = new List<Candidate>();
            for (int i = 0; i < QbQuestionsBank.Count; i++)
            {
                Candidate candi = GetRdQCandidateFromQuestion(listQForShuffle.ElementAt(i).Candidates);
                



                listCandidate.Add(candi);
                listQForShuffle.ElementAt(i).Candidates.Remove(candi);
                if (listQForShuffle.ElementAt(i).Candidates.Count == 0)
                {
                    listQForShuffle.ElementAt(i).Candidates = ResetQuestion(i);
                }
            }
            ExamForDoc ei = new ExamForDoc(codeExam, listCandidate);
            if (IsDuplicated(ei))
            {
                CreateExamItem(listQForShuffle, codeExam);
            }
            return new ExamForDoc(codeExam, listCandidate);
        }

        /// <summary>
        /// Reset after removing all elements in List of Candidates
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        private List<Candidate> ResetQuestion(int i)
        {
            List<Candidate> listQc = new List<Candidate>();
            foreach(Candidate candi in QbQuestionsBank.ElementAt(i).Candidates)
            {
                listQc.Add(candi);
            }
            return listQc;
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
        private bool IsDuplicated(ExamForDoc ei)
        {
            String res = "";
            foreach(Candidate q in ei.ExamQuestionsList)
            {
                res = res + q.QuestionId + q.CandidateId;
            }
            if (EiListForDuplicate.Contains(res))
            {
                return true;
            }
            else
            {
                EiListForDuplicate.Add(res);
                return false;
            }
        }
    }
}
