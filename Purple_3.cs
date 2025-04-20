using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_7
{
    public class Purple_3
    {
        public struct Participant
        {
            private string _name;
            private string _surname;
            private double[] _marks;
            private int[] _places;

            private int _indexN;



            public string Name => _name;
            public string Surname => _surname;
            public double[] Marks
            {
                get
                {
                    if (_marks == null) return null;
                    var copy1 = new double[_marks.Length];
                    Array.Copy(_marks, copy1, _marks.Length);
                    return copy1;
                }
            }
            public int[] Places
            {
                get
                {
                    if (_places == null) return null;
                    var copy2 = new int[_places.Length];
                    Array.Copy(_places, copy2, _places.Length);
                    return copy2;
                }
            }
            public int Score
            {
                get
                {
                    if (_places == null) return 0;
                    return _places.Sum();

                }
            }
            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _marks = new double[7];
                _places = new int[7];
                _indexN = 0;
            }
            public void Evaluate(double result)
            {
                if (_indexN >= 7 || _marks == null) return;
                _marks[_indexN++] = result;
            }
            public static void SetPlaces(Participant[] participants)
            {
                if (participants == null || participants.Length == 0) return;

                for (int i1 = 0; i1 < 7; i1++)
                {
                    InsertionSort(participants, i1);
                    for (int j1 = 0; j1 < participants.Length; j1++)
                    {
                        if (participants[j1].Marks != null)
                        {
                            participants[j1]._places[i1] = j1 + 1;
                        }
                    }
                }
            }
            public static void Sort(Participant[] array)
            {
                if (array == null) return;

                for (int i2 = 1; i2 < array.Length; i2++)
                {
                    var current = array[i2];
                    int j2 = i2 - 1;

                    while (j2 >= 0 && CompareParticipants(array[j2], current) > 0)
                    {
                        array[j2 + 1] = array[j2];
                        j2--;
                    }

                    array[j2 + 1] = current;
                }
            }
            private static void InsertionSort(Participant[] participants, int markIndex)
            {
                for (int i3 = 1; i3 < participants.Length; i3++)
                {
                    var current = participants[i3];
                    int j3 = i3 - 1;

                    while (j3 >= 0 && (participants[j3].Marks == null || participants[j3].Marks[markIndex] < current.Marks[markIndex]))
                    {
                        participants[j3 + 1] = participants[j3];
                        j3--;
                    }

                    participants[j3 + 1] = current;
                }
            }
            private static int CompareParticipants(Participant a, Participant b)
            {
                if (a.Places == null || b.Places == null || a.Marks == null || b.Marks == null)
                    return 0;
                if (a.Score < b.Score)
                    return -1;
                if (a.Score > b.Score)
                    return 1;
                int A = a.Places.Min();
                int B = b.Places.Min();
                if (A < B)
                    return -1;
                if (A > B)
                    return 1;
                double sumA = a.Marks.Sum();
                double sumB = b.Marks.Sum();
                if (sumA > sumB)
                    return -1;
                if (sumA < sumB)
                    return 1;

                return 0;
            }
            public void Print()
            {
                Console.WriteLine($"Name {_name}\tSurname{_surname}\tScore {Score}\tBest place {_places.Min()}\t Marks_sum{Math.Round(_marks.Sum(), 2)}");
            }
        }

        public abstract class Skating
        {
            private Participant[] _participants;
            protected double[] _moods;
            public Participant[] Participants => (_participants == null) ? _participants : (Participant[])_participants.Clone();
            public double[] Moods => (_moods == null) ? _moods : (double[])_moods.Clone();

            protected abstract void ModificateMood();
            public Skating(double[] moods)
            {
                if (moods == null) return;
                _participants = new Participant[0];
                _moods = new double[Math.Min(moods.Length, 7)];
                Array.Copy(moods, _moods, Math.Min(moods.Length, 7));
                ModificateMood();
            }
            public void Evaluate(double[] marks)
            {
                if (marks == null || _participants == null) return;

                int player_index = -1;
                for (int i = 0; i < _participants.Length; i++)
                {
                    var marksArray = _participants[i].Marks;
                    if (marksArray == null) continue;

                    bool allZeros = true;
                    for (int j = 0; j < marksArray.Length; j++)
                    {
                        if (marksArray[j] != 0)
                        {
                            allZeros = false;
                            break;
                        }
                    }
                    if (allZeros)
                    {
                        player_index = i;
                        break;
                    }
                }

                if (player_index <= -1) return;

                var target_player = _participants[player_index];
                for (int i = 0; i < target_player.Marks.Length; i++)
                {
                    target_player.Evaluate(marks[i] * _moods[i]);
                }
                _participants[player_index] = target_player;
            }

            public void Add(Participant player)
            {
                if (_participants == null) _participants = new Participant[0];

                Array.Resize(ref _participants, _participants.Length + 1);
                _participants[_participants.Length - 1] = player;
            }

            public void Add(Participant[] players)
            {
                if (players == null || players.Length == 0) return;


                for (int i = 0; i < players.Length; i++)
                {
                    Add(players[i]);
                }
            }
        }

        public class FigureSkating : Skating
        {
            public FigureSkating(double[] moods) : base(moods) { }

            protected override void ModificateMood()
            {
                if (_moods == null) return;

                for (int i = 0; i < _moods.Length; i++)
                    _moods[i] += (i + 1) * 0.1;
            }
        }

        public class IceSkating : Skating
        {
            public IceSkating(double[] moods) : base(moods) { }

            protected override void ModificateMood()
            {
                if (_moods == null) return;

                for (int i = 0; i < _moods.Length; i++)
                    _moods[i] *= 1 + (i + 1) * 0.01;
            }
        }
    }
}