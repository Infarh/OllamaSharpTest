using System.Text;

using OllamaSharp;

const string url = "http://localhost:11434";
const string model = "gemma3:27b";

using (var ollama2 = new OllamaApiClient(url, model))
{
    var result = new StringBuilder();
    var request = ollama2.GenerateAsync(new()
    {
        Stream = false,
        System = "Ты доктор филологии русского языка. Ты сделаешь порученную тебе работу и не будешь писать никакого другого текста кроме запрошенного.",
        Prompt = "Просклоняй имя человека `Кошельков В.С.`. Создай список из значений: название падежа:значение. Каждый элемент списка должен быть на новой строке. Сохрани инициалы.",
    });

    await foreach (var r in request)
        if (r is { Response: { Length: > 0 } response })
        {
            Console.Write(response);
            result.AppendLine(response);
        }

    var str = result.ToString();
}

using var ollama = new OllamaApiClient(url, model);

var models = await ollama.ListLocalModelsAsync();

foreach (var m in models)
    Console.WriteLine(m.Name);

//await foreach (var status in ollama.PullModelAsync("llama3.1:405b"))
//    Console.WriteLine($"{status.Percent}% {status.Status}");

await foreach (var stream in ollama.GenerateAsync("Как у тебя дела?"))
    Console.Write(stream?.Response);

var chat = new Chat(ollama);

while (true)
{
    Console.Write(">> ");
    if (Console.ReadLine() is not { Length: > 0 } message)
        break;

    await foreach (var answer in chat.SendAsync(message))
        Console.Write(answer);
}


Console.WriteLine("End.");
return;