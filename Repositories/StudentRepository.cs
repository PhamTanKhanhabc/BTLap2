using Dapper;
using System.Data;
using Microsoft.Data.SqlClient;
using DapperApi.Models;

namespace DapperApi.Repositories;

public class StudentRepository : IStudentRepository
{
    private readonly string _connStr;

    public StudentRepository(IConfiguration config)
    {
        _connStr = config.GetConnectionString("DefaultConnection")!;
    }

    // Tao connection moi moi lan goi
    private IDbConnection NewConnection()
        => new SqlConnection(_connStr);

    // GET ALL
    public IEnumerable<Student> GetAll()
    {
        using var db = NewConnection();
        return db.Query<Student>("SELECT * FROM Students");
    }

    // GET BY ID
    public Student? GetById(int id)
    {
        using var db = NewConnection();
        return db.QuerySingleOrDefault<Student>(
            "SELECT * FROM Students WHERE Id = @Id",
            new { Id = id });
    }

    // CREATE
    public void Create(Student student)
    {
        using var db = NewConnection();
        db.Execute(
            "INSERT INTO Students (Name, Age) VALUES (@Name, @Age)",
            student);
    }

    // UPDATE
    public void Update(Student student)
    {
        using var db = NewConnection();
        db.Execute(
            "UPDATE Students SET Name=@Name, Age=@Age WHERE Id=@Id",
            student);
    }

    // DELETE
    public void Delete(int id)
    {
        using var db = NewConnection();
        db.Execute(
            "DELETE FROM Students WHERE Id=@Id",
            new { Id = id });
    }
}