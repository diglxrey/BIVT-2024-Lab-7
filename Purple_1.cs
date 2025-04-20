using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_7
{
    public class Purple_1
    {
        public class Participant
        {
            private string _name;
            private string _surname;
            private double[] _coefs;
            private int[,] _marks;
            private int _jumpN;


            public string Name => _name;
            public string Surname => _surname;
            public double[] Coefs
            {
                get
                {
                    if (_coefs == null) return null;
                    var copy1 = new double[_coefs.Length];
                    Array.Copy(_coefs, copy1, _coefs.Length);
                    return copy1;
                }
            }
            public int[,] Marks
            {
                get
                {
                    if (_marks == null) return null;
                    var copy2 = new int[_marks.GetLength(0), _marks.GetLength(1)];
                    Array.Copy(_marks, copy2, _marks.Length);
                    return copy2;
                }
            }
            public double TotalScore { get; private set; }

            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _coefs = new double[4] { 2.5, 2.5, 2.5, 2.5 };
                _marks = new int[4, 7];
                _jumpN = 0;
                TotalScore = 0;
            }
            public void SetCriterias(double[] coefs)
            {
                if (_coefs == null || coefs == null || coefs.Length != 4) return;
                Array.Copy(coefs, _coefs, coefs.Length);
            }

            public void Jump(int[] marks)
            {
                if (marks == null || _marks == null || _coefs == null || _jumpN >= 4 || marks.Length != 7) return;

                for (int i1 = 0; i1 < marks.Length; i1++) _marks[_jumpN, i1] = marks[i1];

                TotalScore += (marks.Sum() - marks.Min() - marks.Max()) * _coefs[_jumpN];

                _jumpN++;
            }

            public static void Sort(Participant[] array)
            {
                if (array == null) return;

                for (int i2 = 1, j1 = 2; i2 < array.Length;)
                {
                    if (i2 == 0 || array[i2].TotalScore <= array[i2 - 1].TotalScore)
                    {
                        i2 = j1;
                        j1++;
                    }
                    else
                    {
                        var temp = array[i2];
                        array[i2] = array[i2 - 1];
                        array[i2 - 1] = temp;
                        i2--;
                    }
                }
            }
            public void Print()
            {
                Console.WriteLine($"Name {_name}");
                Console.WriteLine($"Surname {_surname}");


                Console.WriteLine("Coefs");
                foreach (var coef in _coefs)
                {
                    Console.Write($"{coef} ");
                }

                Console.WriteLine("Marks");
                int a1 = _marks.GetLength(0);
                int b2 = _marks.GetLength(1);
                for (int i3 = 0; i3 < a1; i3++)
                {
                    for (int j2 = 0; j2 < b2; j2++)
                    {
                        Console.Write($"{_marks[i3, j2]} ");
                    }
                    Console.WriteLine();
                }
                Console.WriteLine($"Total score {TotalScore}");
            }
        }

        public class Judge
        {
            private string _name;
            private int[] _marks;
            public string Name => _name;
            private int _NMark;

            public Judge(string name, int[] marks)
            {
                _name = name;
                if (marks == null) _marks = null;
                else
                {
                    _marks = new int[marks.Length];
                    Array.Copy(marks, _marks, marks.Length);
                }
            }

            public int CreateMark()
            {
                if (_marks == null || _marks.Length == 0) return 0;

                if (_NMark == _marks.Length)
                {
                    _NMark = 0;
                    return _marks[_NMark++];
                }
                else return _marks[_NMark++];

            }

            public void Print()
            {
                if (_marks == null)
                {
                    Console.WriteLine("Error");
                    return;
                }
                Console.WriteLine($"Name {_name}");
                for (int i4 = 0; i4 < _marks.Length; i4++)
                {
                    Console.Write(_marks[i4] + " ");
                }
            }

        }

        public class Competition
        {
            private Judge[] _judges;
            private Participant[] _participants;
            public Judge[] Judges => _judges;
            public Participant[] Participants => (_participants == null) ? _participants : (Participant[])_participants.Clone();

            public Competition(Judge[] judges)
            {
                if (judges != null)
                {
                    _judges = new Judge[judges.Length];
                    Array.Copy(judges, _judges, judges.Length);
                }

                _participants = new Participant[0];
            }

            public void Evaluate(Participant jumper)
            {
                if (jumper == null || _judges == null) return;

                var result_Marks = new int[7];
                int judge_ind = 0;

                for (int i5 = 0; i5 < _judges.Length; i5++)
                {   
                    result_Marks[judge_ind++] = _judges[i5].CreateMark();
                    if (judge_ind >= 7) break;
                }

                jumper.Jump(result_Marks);
            }

            public void Add(Participant player)
            {
                if (player == null) return;
                if (_participants == null) _participants = new Participant[0];
                Array.Resize(ref _participants, _participants.Length + 1);
                _participants[_participants.Length - 1] = player;
                Evaluate(_participants[_participants.Length - 1]);
            }

            public void Add(Participant[] players)
            {
                if (players == null || _participants == null) return;
                
                int n = _participants.Length;
                for (int player_ind = 0; player_ind < players.Length; player_ind++)
                {
                    if (players[player_ind] != null)
                    {
                        Evaluate(players[player_ind]);
                        Array.Resize(ref _participants, _participants.Length + 1);
                        _participants[_participants.Length - 1] = players[player_ind];
                    }
                }
            }

            public void Sort()
            {
                if (_participants == null || _participants.Length <= 1)
                    return;
                Participant.Sort(_participants);
            }
        }
    }
}