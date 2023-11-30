using BeanBoost.Models;
using Microsoft.AspNetCore.Components;
using MongoDB.Driver;

namespace BeanBoost.Pages
{
    public partial class Index
    {
        private List<Student> students;


        protected override async Task OnInitializedAsync()
        {
            students = await DbContext.Students.Find(_ => true).ToListAsync();
        }

        private void AddStudent()
        {
            var newStudent = new Student
            {
                Name = "John Doe",
                Position = 1,
                TimesBought = 0
            };

            DbContext.Students.InsertOne(newStudent);
        }
    }
}
