using BeanBoost.Models;
using Microsoft.AspNetCore.Components;
using MongoDB.Driver;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Mvc;

namespace BeanBoost.Pages.Queue
{
    public partial class Queue
    {
        private List<Student> students = new List<Student>();

        protected override async Task OnInitializedAsync()
        {
            var studentCursor = await DbContext.Students.FindAsync(_ => true);
            students = await studentCursor.ToListAsync();
        }

        private async Task DeleteStudent(Student student)
        {

            foreach(var s in students)
            {
                if (student.Position < s.Position)
                {
                    await DbContext.Students.UpdateOneAsync(
                        Builders<Student>.Filter.Gte("Position", s.Position),
                        Builders<Student>.Update.Inc("Position", -1)
                     );
                }
            }
            
            await DbContext.DeleteStudentAsync(student);

            var studentCursor = await DbContext.Students.FindAsync(_ => true);
            students = await studentCursor.ToListAsync();
            StateHasChanged();
        }

        private async Task Join()
        {
            Nav.NavigateTo("/Join");
        }

        private async Task Edit(Student student)
        {
            Program.student = student;
            Nav.NavigateTo("/Edit");
        }
        public async Task UpdateList()
        {
            

            var studentToMove = students.OrderBy(s => s.Position).FirstOrDefault();

            if (studentToMove != null)
            {
                int highestStudentPosition = students.Max(c => c.Position);

                foreach (var student in students)
                {
                    if (student.Id != studentToMove.Id)
                    {
                        student.Position = student.Position - 1;
                    }
                }

                studentToMove.Position = highestStudentPosition;
                studentToMove.TimesBought++;
            }
            foreach(var student in students)
            {
                var filter = Builders<Student>.Filter.Eq(s => s.Id, student.Id);

                var updatePosition = Builders<Student>.Update.Set(s => s.Position, student.Position);
                var updateTimesBought = Builders<Student>.Update.Set(s => s.TimesBought, student.TimesBought);
                await DbContext.Students.UpdateOneAsync(filter, updatePosition);
                await DbContext.Students.UpdateOneAsync(filter, updateTimesBought);

            }
            StateHasChanged();
        }
    }
}
