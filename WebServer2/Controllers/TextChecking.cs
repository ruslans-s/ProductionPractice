using Microsoft.AspNetCore.Mvc;
using NeuralNetworkML.Model;
using System;
using System.Text.RegularExpressions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebServer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TextChecking : ControllerBase
    {

        public class OutputString
        {
            public string Output { get; set; }
        }
        public class InputString
        {
            public string Input { get; set; }
        }

        private OutputString RequestProcessing(string data)
        {
            data.ToLower(); // Приводим к нижнему регистру

            data = Regex.Replace(data, @"[-.?!)(,:]", " "); //Удаляем знаки пунктуаций

            // Создание входных данных
            ModelInput sampleData = new ModelInput()
            {
                Col0 = data,
            };

            //Получение ответа
            var predictionResult = ConsumeModel.Predict(sampleData);

            //ВЫвод ответа
            if (Math.Abs(predictionResult.Score[0] - predictionResult.Score[1]) <= 0.2)
            {
                return (new OutputString { Output = "Neutral" });
            }
            else if (predictionResult.Prediction == "-1")
            {
                return (new OutputString { Output = "Negative" });
            }
            else
            {
                return (new OutputString { Output = "Positive" });
            }
        }

        //Пустой запрос с "ошибкой"
        [HttpGet]
        public ActionResult<OutputString> Get()
        {
            return (new OutputString { Output = "Error: empty request"});
        }

        [HttpGet("{data}")]
        public ActionResult<OutputString> Get(string data)
        {
            return RequestProcessing(data);
            //return "Прогноз: " + predictionResult.Prediction + " Выходные веса: " +String.Join(",", predictionResult.Score);
        }

        //Запрос через формат JSON
        [HttpGet("jsonquery")]
        public ActionResult<OutputString> GetFromJSON([FromBody] InputString inputJSON)
        {
            if (String.IsNullOrEmpty(inputJSON.Input))
            {
                return (new OutputString { Output = "Empty Query" });
            }
            else
            {
                return RequestProcessing(inputJSON.Input);
            }
        }

    }
}
