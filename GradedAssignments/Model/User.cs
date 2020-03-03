using System.Collections.ObjectModel;
using System.IO;

namespace GradedAssignments.Model
{
    public class User
    {

        public User(string data)
        {
            // Name, Age, Weight, Score 
            var l = data.Split(';');
            Id = l[0];
            Name = l[1];
            Age = int.Parse(l[2]);
            Score = int.Parse(l[3]);
        }


        public string Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public int Score { get; set; }
        public override string ToString()
        {
            return $"ID: {Id, 2}, {Name,-10}";
        }
        public static ObservableCollection<User> ReadCsvFile(string filename)
        {
            var list = new ObservableCollection<User>();
            using (var file = new StreamReader(filename))
            {
                string line;
                while ((line = file.ReadLine()) != null)
                {
                    var u = new User(line);
                    list.Add(u);
                }
            }
            return list;
        }
    }
}
