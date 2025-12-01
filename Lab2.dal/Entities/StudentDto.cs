using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Lab2.dal.Entities
{
    public class StudentDto
    {
        [BsonId] // Це каже Mongo, що це головний ключ
        public string Id { get; set; }

        [BsonElement("Name")]
        public string Name { get; set; }

        [BsonElement("BirthDate")]
        public DateTime Birth { get; set; }
    }
}