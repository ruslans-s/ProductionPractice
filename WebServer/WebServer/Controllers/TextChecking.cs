using Microsoft.AspNetCore.Mvc;
using NeuralNetworkML.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TextChecking : ControllerBase
    {
        // GET: api/<TextChecking>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<TextChecking>/Text
        [HttpGet("{data}")]
        public string Get(string data)
        {
            // Создание входных данных
            ModelInput sampleData = new ModelInput()
            {
                Clear_text = data,
            };
            //Получение ответа
            var predictionResult = ConsumeModel.Predict(sampleData);

            return "Прогноз: " + predictionResult.Prediction + " Выходные веса:" +String.Join(",", predictionResult.Score);
        }

        // POST api/<TextChecking>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<TextChecking>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<TextChecking>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
