using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using PCL.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCL.Infrastructure.Persistence
{
    public class PetCareContext
    {
        private readonly IMongoDatabase _database;

        public PetCareContext(IOptions<MongoDbSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            _database = client.GetDatabase(settings.Value.DatabaseName);
        }

        public IMongoCollection<Pet> Pets => _database.GetCollection<Pet>("pets");
        public IMongoCollection<TypeAnimal> TypeAnimals => _database.GetCollection<TypeAnimal>("typeAnimals");
        public IMongoCollection<Breed> Breeds => _database.GetCollection<Breed>("breeds");
        public IMongoCollection<Gender> Genders => _database.GetCollection<Gender>("genders");
        public IMongoCollection<Owner> Owners => _database.GetCollection<Owner>("owners");
        public IMongoCollection<Veterinarian> Veterinarians => _database.GetCollection<Veterinarian>("veterinarians");
        public IMongoCollection<Appointment> Appointments => _database.GetCollection<Appointment>("appointments");
        public IMongoCollection<Examination> Examinations => _database.GetCollection<Examination>("examination");
        public IMongoCollection<ExaminationResult> ExaminationResults => _database.GetCollection<ExaminationResult>("examinationResults");
        public IMongoCollection<MedicalRecord> MedicalRecords => _database.GetCollection<MedicalRecord>("medicalRecords");
        public IMongoCollection<Employee> Employees => _database.GetCollection<Employee>("employees");
        public IMongoCollection<Service> Services => _database.GetCollection<Service>("services");
        public IMongoCollection<Trainer> Trainers => _database.GetCollection<Trainer>("trainers");
        public IMongoCollection<Training> Trainings => _database.GetCollection<Training>("trainings");
        public IMongoCollection<TrainingRecord> TrainingRecords => _database.GetCollection<TrainingRecord>("trainingRecords");
        public IMongoCollection<TrainingResult> TrainingResults => _database.GetCollection<TrainingResult>("trainingResults");
        public IMongoCollection<Permission> Permissions => _database.GetCollection<Permission>("permissions");
        public IMongoCollection<Role> Roles => _database.GetCollection<Role>("roles");
        public IMongoCollection<User> Users => _database.GetCollection<User>("users");
        public IMongoCollection<Category> Categories => _database.GetCollection<Category>("categories");
        public IMongoCollection<Product> Products => _database.GetCollection<Product>("products");
    }
}
