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

        public List<TestFullInfo> EiListDoc;//Include Exam after create with full infomation

        public List<TestItem> EiListMarking;//Include Exam after create with simple infomation

        /// <summary>
        /// Create List of ExamItem
        /// </summary>
        /// <param name="qbQuestionsBank"></param>
        /// <param name="numOfPage"></param>
        public ShuffleExamModel(List<Question> qbQuestionsBank, int numOfPage)
        {
            QbQuestionsBank = qbQuestionsBank;
            EiListDoc = new List<TestFullInfo>();
            EiListMarking = new List<TestItem>();

            //Create all of cases for these candidates then get numOfPage cases from them.
            List<List<CandidateNode>> Cases = GetRandomNElementsInList(numOfPage, GetAllCasesTest());

            //codeTestCount: for TestCode
            int codeTestCount = 0;
            //Adding candidate into Tests
            foreach (List<CandidateNode> c in Cases)
            {
                List<Candidate> candidateList = new List<Candidate>();
                //Adding candidate into a Test

                foreach (var candidateNode in c)
                {
                    candidateList.Add(candidateNode.Candi);
                }
                var aTest = new TestFullInfo();
                aTest.PaperNo = (++codeTestCount).ToString();
                aTest.ExamQuestionsList = candidateList;
                EiListDoc.Add(aTest);
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
        /// Get random elements in List
        /// </summary>
        /// <param name="numOfCases"></param>
        /// <param name="allCases"></param>
        /// <returns></returns>
        private List<List<CandidateNode>> GetRandomNElementsInList(int numOfCases, List<List<CandidateNode>> allCases)
        {
            List<List<CandidateNode>> newList = new List<List<CandidateNode>>();
            for(int i = 0; i < numOfCases; i++)
            {
                int randNumber = GetRandomNumber(0, allCases.Count);
                newList.Add(allCases.ElementAt(randNumber));
                allCases.RemoveAt(randNumber);
            }
            return newList;
        }

        /// <summary>
        /// Add all cases of the tests
        /// </summary>
        /// <returns></returns>
        private List<List<CandidateNode>> GetAllCasesTest()
        {
            CandidateNode root = SetCandidateNode(null, 0, BuildingTree());
            root.AddPath(root, new List<CandidateNode>());
            return root.paths;
        }

        /// <summary>
        /// Building a tree of Candidates for generate all the cases
        /// </summary>
        /// <returns></returns>
        private int[] BuildingTree()
        {
            var quizs = new int[QbQuestionsBank.Count];
            int i = 0;
            foreach (var question in QbQuestionsBank)
            {
                quizs[i++] = question.Candidates.Count;
            }
            return quizs;
        }

        /// <summary>
        /// Set relation of all Nodes in the Tree
        /// </summary>
        /// <param name="value"></param>
        /// <param name="pos"></param>
        /// <param name="quizs"></param>
        /// <returns></returns>
        public CandidateNode SetCandidateNode(Candidate value, int pos, int[] quizs)
        {
            CandidateNode child = new CandidateNode
            {
                Candi = value
            };
            if (pos < quizs.Length)
            {
                foreach (var candi in QbQuestionsBank.ElementAt(pos).Candidates)
                {
                    child.Children.Add(SetCandidateNode(candi, pos + 1, quizs));
                }
            }
            else
            {
                child.Children = null;
            }
            return child;
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
    }
}
