using BeanBoost.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using MongoDB.Driver;

namespace BeanBoost.Pages.Queue
{
    public partial class Join
    {
        private List<Student> students;
        private string StudentName;

        protected override async Task OnInitializedAsync()
        {
            students = await DbContext.Students.Find(_ => true).ToListAsync();
        }
        private void AddStudent()
        {
            var newStudent = new Student
            {
                Name = StudentName,
                Position = GetPosition(),
                TimesBought = 1
            };

            DbContext.Students.InsertOne(newStudent);
            StateHasChanged();
            Nav.NavigateTo("/Queue");
        }

        private void BackToQueue()
        {
            Nav.NavigateTo("/Queue");
        }

        public int GetPosition()
        {
            int highestPosition = 0;
            foreach(var student in students)
            {
                highestPosition++;
            }
            highestPosition++;
            return highestPosition;
        }
    }
}
