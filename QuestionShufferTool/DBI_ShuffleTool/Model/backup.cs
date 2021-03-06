﻿using System;
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

        public List<string> EiItemCodeList;//Include Code ExamItem

        public List<TestFullInfo> EiListDoc;//Include Exam after create

        public List<TestItem> EiListMarking;//

        public List<string> EiListForDuplicate;//Include ExamCode for checking Duplicated
        /// <summary>
        /// Create List of ExamItem
        /// </summary>
        /// <param name="qbQuestionsBank"></param>
        /// <param name="numOfPage"></param>
        public ShuffleExamModel(List<Question> qbQuestionsBank, int numOfPage)
        {
            QbQuestionsBank = qbQuestionsBank;
            EiItemCodeList = new List<string>();
            EiListDoc = new List<TestFullInfo>();
            EiListForDuplicate = new List<string>();
            EiListMarking = new List<TestItem>();
            for (int i = 0; i < numOfPage; i++)//Create Code of the ExamItem
            {
                EiItemCodeList.Add((i + 1).ToString());
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
            foreach (TestFullInfo eDoc in EiListDoc)
            {
                TestItem eMark = new TestItem();
                List<CandidateSimple> candidateExams = new List<CandidateSimple>();
                foreach (Candidate candi in eDoc.ExamQuestionsList)
                {
                    CandidateSimple candiExam = new CandidateSimple();
                    candiExam.CandidateId = candi.CandidateId;
                    candiExam.DBName = candi.DBName;
                    candiExam.Point = candi.Point;
                    candiExam.QuestionId = candi.QuestionId;
                    candiExam.QuestionRequirement = candi.QuestionRequirement;
                    candiExam.QuestionType = candi.QuestionType;
                    candiExam.RequireSort = candi.RequireSort;
                    candiExam.Solution = candi.Solution;
                    candiExam.TestQuery = candi.TestQuery;
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
        private TestFullInfo CreateExamItem(List<Question> listQForShuffle, String codeExam)
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
            TestFullInfo ei = new TestFullInfo(codeExam, listCandidate);
            if (IsDuplicated(ei))
            {
                return CreateExamItem(listQForShuffle, codeExam);
            }
            else
            {
                return new TestFullInfo(codeExam, listCandidate);
            }
        }

        /// <summary>
        /// Reset after removing all elements in List of Candidates
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        private List<Candidate> ResetQuestion(int i)
        {
            List<Candidate> listQc = new List<Candidate>();
            foreach (Candidate candi in QbQuestionsBank.ElementAt(i).Candidates)
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
        private bool IsDuplicated(TestFullInfo ei)
        {
            string res = "";
            foreach (Candidate q in ei.ExamQuestionsList)
            {
                res = res + q.CandidateId;
            }
            //if (EiListForDuplicate.Contains(res))
            //{
            //    return true;
            //}
            EiListForDuplicate.Add(res);
            return false;
        }
    }
}
