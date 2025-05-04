using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_7
{
    public class Purple_4
    {
        public class Sportsman
        {
            private string _name;
            private string _surname;
            private double _time;

            public string Name => _name;
            public string Surname => _surname;
            public double Time => _time;

            public Sportsman(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _time = 0;
            }

            public void Run(double time)
            {
                if (time > 0 && _time == 0) _time = time;
            }
            public static void Sort(Sportsman[] array)
            {
                if (array == null || array.Length < 2) return;
                for (int i = 1; i < array.Length; i++)
                {
                    var current = array[i];
                    int j = i - 1;

                    while (j >= 0 && array[j]._time > current._time)
                    {
                        array[j + 1] = array[j];
                        j--;
                    }
                    array[j + 1] = current;
                }
            }
            public void Print()
            {
                Console.WriteLine($"Name {_name}   Surname {_surname}    Time {_time}");
                Console.WriteLine();
            }
        }

        public class SkiMan : Sportsman
        {
            public SkiMan(string name, string surname) : base(name, surname) { }
            public SkiMan(string name, string surname, double time) : base(name, surname)
            {
                Run(time);
            }

        }

        public class SkiWoman : Sportsman
        {
            public SkiWoman(string name, string surname) : base(name, surname) { }
            public SkiWoman(string name, string surname, double time) : base(name, surname)
            {
                Run(time);
            }
        }

        public class Group
        {
            private string _name;
            private Sportsman[] _sportsmen;
            public string Name => _name;
            public Sportsman[] Sportsmen => (_sportsmen == null) ? _sportsmen : (Sportsman[])_sportsmen.Clone();

            public Group(string name)
            {
                _name = name;
                _sportsmen = new Sportsman[0];
            }
            public Group(Group group)
            {
                _name = group.Name;

                if (group.Sportsmen == null) _sportsmen = null;
                else
                {
                    _sportsmen = new Sportsman[group.Sportsmen.Length];
                    Array.Copy(group.Sportsmen, _sportsmen, group.Sportsmen.Length);
                }
            }

            public void Add(Sportsman sportsman0)
            {
                if (_sportsmen == null) return;
                Array.Resize(ref _sportsmen, _sportsmen.Length + 1);
                _sportsmen[_sportsmen.Length - 1] = sportsman0;

            }
            public void Add(Sportsman[] sportsmen)
            {
                if (_sportsmen == null || sportsmen.Length == 0 || sportsmen == null) return;
                int indexN = _sportsmen.Length;
                Array.Resize(ref _sportsmen, indexN + sportsmen.Length);
                for (int i1 = indexN; i1 < _sportsmen.Length; i1++)
                {
                    _sportsmen[i1] = sportsmen[i1 - indexN];
                }

            }
            public void Add(Group group)
            {
                if (_sportsmen == null || group.Sportsmen == null) return;
                Add(group.Sportsmen);
            }

            public void Sort()
            {
                if (_sportsmen == null) return;

                for (int i2 = 0; i2 < _sportsmen.Length - 1; i2++)
                {
                    for (int j1 = 0; j1 < _sportsmen.Length - i2 - 1; j1++)
                    {
                        if (_sportsmen[j1].Time > _sportsmen[j1 + 1].Time)
                        {
                            Sportsman temp = _sportsmen[j1];
                            _sportsmen[j1] = _sportsmen[j1 + 1];
                            _sportsmen[j1 + 1] = temp;
                        }
                    }
                }
            }
            public static Group Merge(Group group1, Group group2)
            {
                var group_sum = new Group("Финалисты");
                if (group1.Sportsmen == null && group2.Sportsmen == null) return group_sum;
                else if (group1.Sportsmen == null)
                {
                    Array.Copy(group2._sportsmen, group_sum.Sportsmen, group2._sportsmen.Length);
                    return group_sum;
                }
                else if (group2.Sportsmen == null)
                {
                    Array.Copy(group1._sportsmen, group_sum.Sportsmen, group1._sportsmen.Length);
                    return group_sum;
                }
                Array.Resize(ref group_sum._sportsmen, group1._sportsmen.Length + group2._sportsmen.Length);
                for (int i3 = 0, j2 = 0, k1 = 0; i3 < group1.Sportsmen.Length || j2 < group2._sportsmen.Length;)
                {
                    if (i3 < group1.Sportsmen.Length && j2 < group2._sportsmen.Length)
                    {
                        if (group1._sportsmen[i3].Time <= group2._sportsmen[j2].Time)
                            group_sum._sportsmen[k1++] = group1._sportsmen[i3++];
                        else group_sum._sportsmen[k1++] = group2._sportsmen[j2++];
                    }
                    else if (i3 < group1._sportsmen.Length)
                        group_sum._sportsmen[k1++] = group1._sportsmen[i3++];
                    else if (j2 < group2._sportsmen.Length)
                        group_sum._sportsmen[k1++] = group2._sportsmen[j2++];
                }
                return group_sum;
            }

            public void Split(out Sportsman[] men, out Sportsman[] women)
            {
                if (_sportsmen == null)
                {
                    men = null;
                    women = null;
                    return;
                }
                //    int menCount = 0;
                //    int womenCount = 0;

                //    foreach (var sportsman in _sportsmen)
                //    {
                //        if (sportsman is SkiMan)
                //            menCount++;
                //        else if (sportsman is SkiWoman)
                //            womenCount++;
                //    }

                //    men = new SkiMan[menCount];
                //    women = new SkiWoman[womenCount];

                //    int menIndex = 0;
                //    int womenIndex = 0;

                //    foreach (var sportsman in _sportsmen)
                //    {
                //        if (sportsman is SkiMan man)
                //            men[menIndex++] = man;
                //        else if (sportsman is SkiWoman woman)
                //            women[womenIndex++] = woman;
                //    }
                men = _sportsmen.Where(s => s is SkiMan).ToArray();
                women = _sportsmen.Where(s => s is SkiWoman).ToArray();
            }

            public void Shuffle()
            {
                if (_sportsmen == null || _sportsmen.Length == 0)
                    return;
                Sort();
                Split(out Sportsman[] men, out Sportsman[] women);

                if (men == null || women == null || men.Length == 0 || women.Length == 0)
                    return;
                women = women.OrderBy(s => s.Time).ToArray();
                men = men.OrderBy(s => s.Time).ToArray();

                int i = 0, j = 0, k = 0;

                bool who_first = men[0].Time <= women[0].Time;

                while (i < men.Length && j < women.Length)
                {
                    if (who_first)
                    {
                        _sportsmen[k++] = men[i++];
                        _sportsmen[k++] = women[j++];
                    }
                    else
                    {
                        _sportsmen[k++] = women[j++];
                        _sportsmen[k++] = men[i++];
                    }
                }

                while (i < men.Length)
                    _sportsmen[k++] = men[i++];

                while (j < women.Length)
                    _sportsmen[k++] = women[j++];

            }

            public void Print()
            {
                Console.WriteLine($"Name {_name}");
                Console.WriteLine();
                Console.WriteLine("Sportsmen");

                foreach (var i4 in _sportsmen)
                {
                    i4.Print();
                }
            }

        }
    }
}