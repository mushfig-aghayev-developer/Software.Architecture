using System;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Abstract.Factory.Pattern
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine("═══════════════════════════════════════════════════════════");
            Console.WriteLine("          Abstract Factory Pattern Demo");
            Console.WriteLine("═══════════════════════════════════════════════════════════");
            Console.WriteLine();

            // Create a sample Person object
            var person = new Person
            {
                Id = 1,
                Name = "John",
                LastName = "Doe"
            };

            Console.WriteLine("Created Person object:");
            Console.WriteLine($"ID: {person.Id}, Name: {person.Name}, LastName: {person.LastName}");
            Console.WriteLine();

            // Demonstrate JSON Factory
            Console.WriteLine("━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");
            Console.WriteLine("Using JSON Abstract Factory");
            Console.WriteLine("━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");
            
            SerializeDeserialize<Person> jsonFactory = new JsonSerializeDeserialize<Person>();
            Console.WriteLine("Created JsonSerializeDeserialize factory");
            
            var jsonTransactionManager = new TransactionManagerV<Person>(jsonFactory);
            Console.WriteLine("Created TransactionManagerV with JSON factory");
            Console.WriteLine();

            Console.WriteLine("Running transaction (Serialize → Deserialize):");
            try
            {
                jsonTransactionManager.Run(person);
                Console.WriteLine("Transaction completed successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            Console.WriteLine();

            // Demonstrate direct usage of factory methods
            Console.WriteLine("━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");
            Console.WriteLine("Direct Factory Method Usage");
            Console.WriteLine("━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");
            
            var serializer = jsonFactory.CreateSerialize();
            Console.WriteLine("Created serializer using factory.CreateSerialize()");
            
            var serializedData = serializer.Serialize(person);
            Console.WriteLine($"Serialized data: {serializedData}");
            Console.WriteLine();

            var deserializer = jsonFactory.CreateDeserialize();
            Console.WriteLine("Created deserializer using factory.CreateDeserialize()");
            
            var deserializedPerson = deserializer.Deserialize(serializedData);
            Console.WriteLine($"Deserialized Person:");
            Console.WriteLine($"   ID: {deserializedPerson.Id}, Name: {deserializedPerson.Name}, LastName: {deserializedPerson.LastName}");
            Console.WriteLine();

            // Demonstrate XML Factory (will show NotImplementedException)
            Console.WriteLine("━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");
            Console.WriteLine("Using XML Abstract Factory");
            Console.WriteLine("━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");
            
            SerializeDeserialize<Person> xmlFactory = new XMLSerializeDeserialize<Person>();
            Console.WriteLine("Created XMLSerializeDeserialize factory");
            
            var xmlTransactionManager = new TransactionManagerV<Person>(xmlFactory);
            Console.WriteLine("Created TransactionManagerV with XML factory");
            Console.WriteLine();

            Console.WriteLine("Running transaction (Serialize → Deserialize):");
            try
            {
                xmlTransactionManager.Run(person);
                Console.WriteLine("Transaction completed successfully!");
            }
            catch (NotImplementedException ex)
            {
                Console.WriteLine($"XML serialization not implemented: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            Console.WriteLine();

            // Summary
            Console.WriteLine("━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");
            Console.WriteLine("   Abstract Factory Pattern Benefits:");
            Console.WriteLine("   Client code (TransactionManagerV) doesn't know concrete implementations");
            Console.WriteLine("   Easy to switch between JSON and XML factories");
            Console.WriteLine("   Factory ensures compatible serializer/deserializer pairs");
            Console.WriteLine("   Follows Open/Closed Principle - open for extension, closed for modification");
            Console.WriteLine("━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");
        }
    }

    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
    }

    public interface ISerialize<T>
    {
        string Serialize(T obj);
    }
    public interface IDeserialize<T>
    {
        T Deserialize(string obj);
    }

    public abstract class SerializeDeserialize<T>
    {
        public abstract ISerialize<T> CreateSerialize();
        public abstract IDeserialize<T> CreateDeserialize();
    }

    public class JsonSerialize<T> : ISerialize<T>
    {
        public string Serialize(T obj)
        {
            return JsonSerializer.Serialize(obj);
        }
    }
    public class JsonDeserialize<T> : IDeserialize<T>
    {
        public T Deserialize(string obj)
        {
            return JsonSerializer.Deserialize<T>(obj) ?? throw new InvalidOperationException("Deserialization resulted in a null value.");
        }
    }

    public class XMLSerialize<T> : ISerialize<T>
    {
        public string Serialize(T obj)
        {
            // XML serialization would be implemented here
            // For now, using JSON as placeholder - replace with actual XML serialization
            throw new NotImplementedException("XML serialization not yet implemented");
        }
    }
    public class XMLDeserialize<T> : IDeserialize<T>
    {
        public T Deserialize(string obj)
        {
            // XML deserialization would be implemented here
            // For now, throwing exception - replace with actual XML deserialization
            throw new NotImplementedException("XML deserialization not yet implemented");
        }
    }

    public class JsonSerializeDeserialize<T> : SerializeDeserialize<T>
    {
        public override IDeserialize<T> CreateDeserialize()
        {
            return  new JsonDeserialize<T>();
        }

        public override ISerialize<T> CreateSerialize()
        {
            return new JsonSerialize<T>();
        }
    }

    public class XMLSerializeDeserialize<T> : SerializeDeserialize<T>
    {
        public override IDeserialize<T> CreateDeserialize()
        {
            return new XMLDeserialize<T>();
        }

        public override ISerialize<T> CreateSerialize()
        {
            return new XMLSerialize<T>();
        }
    }

    public class TransactionManagerV<T>
    {
        public readonly SerializeDeserialize<T> _serializeDeserialize;

        public TransactionManagerV(SerializeDeserialize<T> serializeDeserialize)
        {
            _serializeDeserialize = serializeDeserialize;
        }

        public void Run(T obj)
        {
            Console.WriteLine("   → Starting serialization...");
            string data = Serialize(obj);
            Console.WriteLine($"   → Serialization complete. Data length: {data.Length} characters");
            Console.WriteLine("   → Starting deserialization...");
            T deserializedObj = Deserialize(data);
            Console.WriteLine("   → Deserialization complete.");
        }

        private string Serialize(T obj)
        {
            var serializer = _serializeDeserialize.CreateSerialize();
            return serializer.Serialize(obj);
        }

        private T Deserialize(string data)
        {
            var deserializer = _serializeDeserialize.CreateDeserialize();
            return deserializer.Deserialize(data);
        }
    }
    #region TransactionManagerV2
    public class TransactionManagerV2<T>
    {
        public void Run(T obj)
        {
            string data = Serialize(obj);
            Deserialize(data);
        }

        private string Serialize(T obj)
        {
            return JsonSerializer.Serialize(obj);
        }

        private T Deserialize(string json)
        {
            return JsonSerializer.Deserialize<T>(json) ?? throw new InvalidOperationException("Deserialization resulted in a null value.");
        }
    } 
    #endregion
}