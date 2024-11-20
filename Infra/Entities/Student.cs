namespace Api_Project_Prn.Infra.Entities
{
    public class Student
    {
        public string Name { get; set; }
        public Scores Score { get; set; }

        public double AverageScore => (Score.Math + Score.Physic + Score.Chemistry) / 3.0;

        public Student(string name, double math, double physic, double chemistry)
        {
            Name = name;
            Score = new Scores
            {
                Math = math,
                Physic = physic,
                Chemistry = chemistry
            };
        }
    }

    public class Scores
    {
        public double Math { get; set; }
        public double Physic { get; set; }
        public double Chemistry { get; set; }
    }

}
