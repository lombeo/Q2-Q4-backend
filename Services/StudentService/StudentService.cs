using Api_Project_Prn.Infra.Entities;

namespace Api_Project_Prn.Services.StudentService
{
    public interface IStudentService
    {
        Task<List<Student>> SortStudents(List<Student> students);
        Task<Student> FindStudentWithAverageScore(List<Student> students, double targetAverage);
    }

    public class StudentService : IStudentService
    {
        List<Student> QuickSort(List<Student> students, int left, int right)
        {
            if (left < right)
            {
                int pivotIndex = Partition(students, left, right);

                QuickSort(students, left, pivotIndex - 1);
                QuickSort(students, pivotIndex + 1, right);
            }

            return students;
        }
        int Partition(List<Student> students, int left, int right)
        {
            var pivot = students[right];
            int i = left - 1;

            for (int j = left; j < right; j++)
            {
                if (students[j].AverageScore > pivot.AverageScore ||
                    (students[j].AverageScore == pivot.AverageScore &&
                     string.Compare(students[j].Name, pivot.Name, StringComparison.Ordinal) < 0))
                {
                    i++;
                    Swap(students, i, j);
                }
            }
            Swap(students, i + 1, right);
            return i + 1;
        }

        void Swap(List<Student> students, int index1, int index2)
        {
            var temp = students[index1];
            students[index1] = students[index2];
            students[index2] = temp;
        }

        public async Task<List<Student>> SortStudents(List<Student> students)
        {
            students = QuickSort(students, 0, students.Count - 1);
            return students;
        }

        public async Task<Student> FindStudentWithAverageScore(List<Student> students, double targetAverage)
        {
            int left = 0;
            int right = students.Count - 1;

            while (left <= right)
            {
                int mid = left + (right - left) / 2;
                double midAverage = students[mid].AverageScore;

                if (Math.Abs(midAverage - targetAverage) < 0.0001)
                {
                    return students[mid];
                }
                else if (midAverage < targetAverage)
                {
                    right = mid - 1;
                }
                else
                {
                    left = mid + 1;
                }
            }

            return null;
        }
    }
}
