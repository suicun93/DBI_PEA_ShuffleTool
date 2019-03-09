using System;
using System.Collections.Generic;
using System.Linq;
using DBI_ShuffleTool.Entity;
using DBI_ShuffleTool.Entity.Paper;
using DBI_ShuffleTool.Entity.Question;

namespace DBI_ShuffleTool.Model
{
    class ShuffleExamModel
    {
        public QuestionSet QuestionSet;//QBank from Creator

        public PaperSet PaperSet;//Include PaperSet after create 

        /// <summary>
        /// Create List of PaperSet
        /// </summary>
        /// <param name="questionSet"></param>
        /// <param name="numOfPage"></param>
        public ShuffleExamModel(QuestionSet questionSet, int numOfPage)
        {
            QuestionSet = questionSet;
            PaperSet = new PaperSet(new List<Paper>(), QuestionSet.DBScriptList);

            //Create all of cases for these candidates then get numOfPage cases from them.
            List<List<CandidateNode>> Cases = GetRandomNElementsInList(numOfPage, GetAllCasesTest());
            //List<List<CandidateNode>> tmp = GetAllCasesTest();
            //List<List<CandidateNode>> Cases = new List<List<CandidateNode>>();
            //Cases.Add(tmp.First());
            //Cases.Add(tmp.Last());


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
                var aTest = new Paper();
                aTest.PaperNo = (++codeTestCount).ToString();
                aTest.CandidateSet = candidateList;
                PaperSet.Papers.Add(aTest);
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
            var quizs = new int[QuestionSet.QuestionList.Count];
            int i = 0;
            foreach (var question in QuestionSet.QuestionList)
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
                foreach (var candi in QuestionSet.QuestionList.ElementAt(pos).Candidates)
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
