﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Windows.Documents;
using DBI_ShuffleTool.Entity;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DBI_ShuffleTool.Model
{
    class ShuffleExamModel
    {
        private QuestionsBank qbQuestionsBank;//QBank from Creator

        private List<String> eiItemCodeList;//Include Code ExamItem

        private List<ExamItem> eiList;//Include Exam after create

        private List<String> eiQuestionsCodeList;//

        public ShuffleExamModel(QuestionsBank qbQuestionsBank, int numOfPage)
        {
            this.qbQuestionsBank = qbQuestionsBank;
            eiItemCodeList = new List<string>();
            eiList = new List<ExamItem>();
            eiQuestionsCodeList = new List<string>();
            for (int i = 0; i < numOfPage; i++)//Create Code of the ExamItem
            {
                eiItemCodeList.Add(getRdCodeForExam());
            }
            Dictionary<int, List<QuestionCandidate>> dicQuestion = new Dictionary<int, List<QuestionCandidate>>();
            dicQuestion = createDicQuestions(numOfPage, dicQuestion);

            createExamItemList(numOfPage, qbQuestionsBank.QBank.Count, dicQuestion);
        }

        //Create Dictionary Questions for a Test
        private Dictionary<int, List<QuestionCandidate>> createDicQuestions(int numOfPage, 
            Dictionary<int, List<QuestionCandidate>> dicQuestion)
        {
            //Dictionary<CodeExam, Questions>
            for (int i = 0; i < qbQuestionsBank.QBank.Count; i++)//i: Question number i
            {
                //qcList: Candidates of that Question i
                List<QuestionCandidate> qcList = new List<QuestionCandidate>();
                foreach (var qc in qbQuestionsBank.QBank.ElementAt(i).ListQCandidate)
                {
                    qcList.Add(qc);
                }

                //qcForExamItems: Store Question Candidate for That ExamItem
                List<QuestionCandidate> qcForExamItems = new List<QuestionCandidate>();

                //Insert candidate into Question i
                for (int j = 0; j < numOfPage; j++)
                {
                    QuestionCandidate qc = getRdQCandidateFromQuestion(ref qcList);
                    qcForExamItems.Add(qc);
                    qcList.Remove(qc);
                    if (qcList.Count == 0)
                    {
                        foreach (var candidate in qbQuestionsBank.QBank.ElementAt(i).ListQCandidate)
                        {
                            qcList.Add(candidate);
                        }
                    }
                }

                if (isDuplicated(qcForExamItems))
                {
                    Dictionary<int, List<QuestionCandidate>> newDic = createDicQuestions(1, dicQuestion);
                    KeyValuePair<int, List<QuestionCandidate>> kvp = newDic.ElementAt(0);
                    dicQuestion.Add(kvp.Key, kvp.Value);
                }
                else
                {
                    dicQuestion.Add(i, qcForExamItems);
                }
            }
            return dicQuestion;
        }

        //Create ExamItem
        private void createExamItemList(int numOfPage, int numOfQuestions, Dictionary<int, List<QuestionCandidate>> dictionary)
        {
            //i: count ExamTest
            //j: Question number j
            for (int i = 0; i < numOfPage; i++)
            {
                List<QuestionCandidate> qcListOfExamItem = new List<QuestionCandidate>();
                for (int j = 0; j < qbQuestionsBank.QBank.Count; j++)
                {
                    qcListOfExamItem.Add(dictionary[j].ElementAt(i));
                }
                ExamItem ei = new ExamItem(eiItemCodeList.ElementAt(i), qcListOfExamItem);
                eiList.Add(ei);
            }
        }

        //Return examItemList
        public List<ExamItem> getExamItemsList()
        {
            return eiList;
        }

        //Return Json ExamItemList
        public String getJsonExamItemsList()
        {
            return JsonConvert.SerializeObject(eiList);
        }
        
        //Create CodeExamItem
        public String getRdCodeForExam()
        {
            String res;
            //Random a code which is not duplicated in examItemCodeList
            do
            {
                res = getRandomNumber(100000, 999999).ToString();
            } while (eiItemCodeList.Contains(res));
            return res;
        }

        //Get a random number from min to max
        private int getRandomNumber(int min, int max)
        {
            return new Random().Next(min, max);
        }


        //Get a random QuestionCandidate From a Question
        public QuestionCandidate getRdQCandidateFromQuestion(ref List<QuestionCandidate> qcList)
        {
            if (qcList.Count == 1)
            {
                return qcList.ElementAt(0);
            }
            shuffleList(qcList);
            return qcList.ElementAt(0);
        }

        //Shuffle a List
        public void shuffleList(List<QuestionCandidate> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = getRandomNumber(0, n + 1);
                QuestionCandidate value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }



        //check is a QuestionsList duplicated
        private bool isDuplicated(List<QuestionCandidate> newListQC)
        {
            String qCodeList = "";
            foreach (QuestionCandidate candidate in newListQC)
            {
                qCodeList = qCodeList + candidate.QCandidateName;
            }
            if (eiQuestionsCodeList.Contains(qCodeList))
            {
                return true;
            }
            eiQuestionsCodeList.Add(qCodeList);
            Console.WriteLine(qCodeList);
            return false;
        }
    }
}
