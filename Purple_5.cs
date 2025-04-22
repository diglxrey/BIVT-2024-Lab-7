using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_7
{
    public class Purple_5
    {
        public struct Response
        {
            private string _animal;
            private string _characterTrait;
            private string _concept;


            public string Animal => _animal;
            public string CharacterTrait => _characterTrait;
            public string Concept => _concept;

            public Response(string animal, string charactertrait, string concept)
            {
                _animal = animal;
                _characterTrait = charactertrait;
                _concept = concept;
            }
            public int CountVotes(Response[] responses, int questionNumber)
            {
                if (responses == null || questionNumber <= 0 || questionNumber >= 4) return 0;

                switch (questionNumber)
                {
                    case 1:
                        string answer = _animal;
                        return answer != null ? responses.Count(a => a.Animal != null && a.Animal == answer) : 0;
                    case 2:
                        string answer2 = _characterTrait;
                        return answer2 != null ? responses.Count(a => a.CharacterTrait != null && a.CharacterTrait == answer2) : 0;
                    case 3:
                        string answer3 = _concept;
                        return answer3 != null ? responses.Count(a => a.Concept != null && a.Concept == answer3) : 0;
                    default:
                        return 0;
                }
            }
            public void Print()
            {
                Console.WriteLine($"Animal {_animal} \t Character Trait {_characterTrait} \t Concept {_concept} \t");
            }
        }
        public struct Research
        {
            private string _name;
            private Response[] _responses;
            public string Name => _name;
            public Response[] Responses
            {
                get
                {
                    if (_responses == null) return null;
                    return _responses;
                }
            }
            public Research(string name)
            {
                _name = name;
                _responses = new Response[0];

            }
            public void Add(string[] answers)
            {
                if (_responses == null || answers.Length < 3 || answers == null) return;
                Array.Resize(ref _responses, _responses.Length + 1);
                _responses[_responses.Length - 1] = new Response(answers[0], answers[1], answers[2]);
            }

            public string[] GetTopResponses(int question)
            {
                if ((question <= 0 || question >= 4) || _responses == null) return null;
                (string Value, int Count)[] valueCounts = new (string, int)[0];
                foreach (var response in _responses)
                {
                    string value = GetValueByQuestion(response, question);
                    if (value != null && value.Length > 0)
                    {
                        bool found = false;
                        for (int i1 = 0; i1 < valueCounts.Length; i1++)
                        {
                            if (valueCounts[i1].Value == value)
                            {
                                valueCounts[i1].Count++;
                                found = true;
                                break;
                            }
                        }
                        if (!found)
                        {
                            Array.Resize(ref valueCounts, valueCounts.Length + 1);
                            valueCounts[valueCounts.Length - 1] = (value, 1);
                        }
                    }
                }
                InsertionSort(ref valueCounts);
                int topCount = Math.Min(5, valueCounts.Length);
                string[] topResults = new string[topCount];
                for (int i2 = 0; i2 < topCount; i2++)
                {
                    topResults[i2] = valueCounts[i2].Value;
                }
                return topResults;
            }
            private string GetValueByQuestion(Response response, int question)
            {
                switch (question)
                {
                    case 1:
                        return response.Animal;
                    case 2:
                        return response.CharacterTrait;
                    case 3:
                        return response.Concept;
                    default:
                        return null;
                }
            }
            private void InsertionSort(ref (string Value, int Count)[] array)
            {
                for (int i3 = 1; i3 < array.Length; i3++)
                {
                    var current = array[i3];
                    int j1 = i3 - 1;
                    while (j1 >= 0 && array[j1].Count < current.Count)
                    {
                        array[j1 + 1] = array[j1];
                        j1--;
                    }
                    array[j1 + 1] = current;
                }
            }
            public void Print()
            {
                for (int i4 = 1; i4 <= 3; i4++)
                {
                    string[] result = GetTopResponses(i4);

                    for (int j2 = 0; j2 < result.Length; j2++)
                    {
                        Console.Write(result[j2] + " ");
                    }
                    Console.WriteLine();
                }
            }
        }

        public class Report
        {
            private Research[] _research;
            private static int _number;
            public Research[] Researches => (_research == null) ? _research : (Research[])_research.Clone();

            static Report()
            {
                _number = 1;
            }

            public Report()
            {
                _research = new Research[0];
            }

            public Research MakeResearch()
            {
                var date = DateTime.Now.ToString("MM/yy");

                var new_R = new Research($"No_{_number}_{date}");

                Array.Resize(ref _research, _research.Length + 1);

                _research[_research.Length - 1] = new_R;
                _number++;
                return new_R;
            }

            private string Answer(Response response, int question)
            {
                switch (question)
                {
                    case 1: 
                        return response.Animal;
                    case 2: 
                        return response.CharacterTrait;
                    case 3: 
                        return response.Concept;
                    default: 
                        return null;
                }
            }

            public (string, double)[] GetGeneralReport(int question)
            {
                if (question < 1 || question > 3 || _research == null)
                    return null;

                int t_Count = 0;
                foreach (var research in _research)
                {
                    if (research.Responses != null)
                    {
                        foreach (var response in research.Responses)
                        {
                            string value = Answer(response, question);
                            if (value != null)
                                t_Count++;
                        }
                    }
                }

                if (t_Count == 0)
                    return Array.Empty<(string, double)>();
                string[] All_Values = new string[t_Count];
                int index = 0;
                foreach (var research in _research)
                {
                    if (research.Responses != null)
                    {
                        foreach (var response in research.Responses)
                        {
                            string value = Answer(response, question);
                            if (value != null)
                                All_Values[index++] = value;
                        }
                    }
                }
                for (int i = 0; i < All_Values.Length - 1; i++)
                {
                    for (int j = 0; j < All_Values.Length - i - 1; j++)
                    {
                        if (string.Compare(All_Values[j], All_Values[j + 1]) > 0)
                        {
                            string temp = All_Values[j];
                            All_Values[j] = All_Values[j + 1];
                            All_Values[j + 1] = temp;
                        }
                    }
                }

                int U_Count = 0;
                string[] U_Values = new string[All_Values.Length];
                int[] counts = new int[All_Values.Length];

                if (All_Values.Length > 0)
                {
                    U_Values[0] = All_Values[0];
                    counts[0] = 1;
                    U_Count = 1;

                    for (int i = 1; i < All_Values.Length; i++)
                    {
                        if (All_Values[i] == All_Values[i - 1])
                        {
                            counts[U_Count - 1]++;
                        }
                        else
                        {
                            U_Values[U_Count] = All_Values[i];
                            counts[U_Count] = 1;
                            U_Count++;
                        }
                    }
                }
                for (int i = 1; i < U_Count; i++)
                {
                    string currentVal = U_Values[i];
                    int currentCount = counts[i];
                    int j = i - 1;

                    while (j >= 0 && counts[j] < currentCount)
                    {
                        U_Values[j + 1] = U_Values[j];
                        counts[j + 1] = counts[j];
                        j--;
                    }

                    U_Values[j + 1] = currentVal;
                    counts[j + 1] = currentCount;
                }
                var result = new (string, double)[U_Count];
                for (int i = 0; i < U_Count; i++)
                {
                    result[i] = (U_Values[i], (double)counts[i] / t_Count * 100);
                }
                return result;
            }

            private Response[] GetAllValidResponses(int question)
            {
                int count = 0;
                foreach (var research in _research)
                {
                    if (research.Responses != null)
                    {
                        foreach (var response in research.Responses)
                        {
                            if (Answer(response, question) != null)
                            {
                                count++;
                            }
                        }
                    }
                }

                Response[] result = new Response[count];
                int index = 0;

                foreach (var research in _research)
                {
                    if (research.Responses != null)
                    {
                        foreach (var response in research.Responses)
                        {
                            if (Answer(response, question) != null)
                            {
                                result[index++] = response;
                            }
                        }
                    }
                }

                return result;
            }
        }
    }
}