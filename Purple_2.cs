using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_7
{
    public class Purple_2
    {
        public struct Participant
        {
            private string _name;
            private string _surname;
            private int _distance;
            private int[] _marks;


            public string Name => _name;
            public string Surname => _surname;
            public int Distance => _distance;
            public int[] Marks
            {
                get
                {
                    if (_marks == null) return null;
                    var copy1 = new int[_marks.Length];
                    Array.Copy(_marks, copy1, _marks.Length);
                    return copy1;
                }
            }
            public int Result { get; private set; }

            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _distance = 0;
                _marks = new int[5];
                Result = 0;
            }
            public void Jump(int distance, int[] marks, int target)
            {
                if (marks == null || marks.Length != 5 || _marks == null) return;
                for (int i1 = 0; i1 < 5; i1++)
                    _marks[i1] = marks[i1];
                Result = marks.Sum() - marks.Min() - marks.Max() + 60 + (distance - target) * 2;
                if (Result < 0) Result = 0;
            }
            public static void Sort(Participant[] array)
            {
                if (array == null) return;
                for (int i2 = 1, j1 = 2; i2 < array.Length;)
                {
                    if (i2 == 0 || array[i2].Result <= array[i2 - 1].Result)
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
                Console.WriteLine($"Name {_name}    Surname {_surname}    Result {Result}");
            }
        }

        public abstract class SkiJumping
        {
            private string _name;
            private int _standard;
            private Participant[] _participants;
            public string Name => _name;
            public int Standard => _standard;
            public Participant[] Participants => (_participants == null) ? _participants : (Participant[])_participants.Clone();

            public SkiJumping(string name, int standard)
            {
                _name = name;
                _standard = standard;
                _participants = new Participant[0];
            }

            public void Add(Participant player)
            {
                if (_participants == null) _participants = new Participant[0];
                Array.Resize(ref _participants, _participants.Length + 1);
                _participants[_participants.Length - 1] = player;
            }

            public void Add(Participant[] players)
            {
                if (_participants == null || players == null) return;

                for (int player_index = 0; player_index < players.Length; player_index++)
                {
                    Add(players[player_index]);
                }
            }

            public void Jump(int distance, int[] marks)
            {
                int player_index = -1;
                for (int i3 = 0; i3 < _participants.Length; i3++)
                {
                    if (_participants[i3].Marks.Sum() == 0 && _participants[i3].Marks != null) 
                    {
                        player_index = i3;
                        break; 
                    }
                }
                if (player_index <= -1) return;

                _participants[player_index].Jump(distance, marks, _standard);
            }


            public void Print()
            {
                if (_participants == null || _name == null || _standard == 0) { Console.WriteLine("Error"); return; }
                Console.WriteLine($"Name {_name}");
                Console.WriteLine($"Standard {_standard}");
                Console.WriteLine($"list_of_players");
                for (int player_index = 0; player_index < _participants.Length; player_index++)
                {
                    _participants[player_index].Print();
                }
                Console.WriteLine();
            }


        }

        public class JuniorSkiJumping : SkiJumping
        {
            public JuniorSkiJumping() : base("100m", 100) { }

        }

        public class ProSkiJumping : SkiJumping
        {
            public ProSkiJumping() : base("150m", 150) { }
        }
    }
}