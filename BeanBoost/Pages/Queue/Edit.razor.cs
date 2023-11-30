using BeanBoost.Models;
using Microsoft.AspNetCore.Components;
using MongoDB.Driver;

namespace BeanBoost.Pages.Queue
{
    public partial class Edit
    {

        private async Task Update()
        {
            await DbContext.Students.UpdateOneAsync
            (
                Builders<Student>.Filter.Eq(s => s.Id, Program.student.Id),
                Builders<Student>.Update
                .Set(s => s.Name, Program.student.Name)
                .Set(s => s.Position, Program.student.Position)
                .Set(s => s.TimesBought, Program.student.TimesBought)
            );
            Nav.NavigateTo("/Queue");
        }

        private async Task Back()
        {
            Nav.NavigateTo("/Queue");
        }
    }
}
