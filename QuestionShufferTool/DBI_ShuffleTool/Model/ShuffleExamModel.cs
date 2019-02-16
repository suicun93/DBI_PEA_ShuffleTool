using System;
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


        public ShuffleExamModel(QuestionsBank qbQuestionsBank, int numOfPage)
        {
            this.qbQuestionsBank = qbQuestionsBank;
            eiItemCodeList = new List<string>();
            eiList = new List<ExamItem>();
            for (int i = 0; i < numOfPage; i++)//Create Code of the ExamItem
            {
                eiItemCodeList.Add(getRdCodeForExam());
            }
            //Dictionary<CodeExam, Questions>
            var dicQuestion = new Dictionary<int, List<QuestionCandidate>>();
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
                    QuestionCandidate qc = getRdQCandidateFromQuestion(qcList);
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
                dicQuestion.Add(i, qcForExamItems);
            }

            createExamItemList(numOfPage, qbQuestionsBank.QBank.Count, dicQuestion);
            

            
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
        public QuestionCandidate getRdQCandidateFromQuestion(List<QuestionCandidate> qcList)
        {
            return qcList.ElementAt(getRandomNumber(0, qcList.Count));
        }

        private bool isDuplicatedQCandidateList(ExamItem newExamItem)
        {
            foreach (ExamItem examItem in eiList)
            {
               
                    var firstNotSecond = newExamItem.ExamQuestionsList.Except(examItem.ExamQuestionsList).ToList();
                    var secondNotFirst = examItem.ExamQuestionsList.Except(newExamItem.ExamQuestionsList).ToList();
                    return !firstNotSecond.Any() && !secondNotFirst.Any();
            }
            return false;
        }

        
    }
}
