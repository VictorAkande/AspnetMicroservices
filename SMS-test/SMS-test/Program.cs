using System.Text;

class Program
{
    static async Task Main(string[] args)
    {
        // SMPP server connection details
        string host = "164.177.157.232";
        int port = 9022;
        string systemType = "SMPP";
        string username = "PPAI";
        string password = "z.7f!Plk";

        // Recipient's phone number
        string toPhoneNumber = "+1234567890"; // Replace with the recipient's phone number

        // Your source address (sender ID)
        string from = "YourSenderID"; // Replace with your sender ID

        // Message to send
        string message = "Hello, this is a test message.";

        // SMPP client configuration
        var config = new SmppClientConfiguration
        {
            Host = host,
            Port = port,
            SystemType = systemType,
            Username = username,
            Password = password,
            InterfaceVersion = SmppInterfaceVersion.V34,
            KeepAlive = true
        };

        // Create and start the SMPP client
        using (var client = new SmppClient(config))
        {
            // Hook up event handlers (optional)
            client.evConnected += (sender, ev) => Console.WriteLine("Connected to SMPP server");
            client.evDisconnected += (sender, ev) => Console.WriteLine("Disconnected from SMPP server");

            // Connect to the SMPP server
            await client.ConnectAsync();

            // Check if the connection was successful
            if (client.Connected)
            {
                // Prepare a simple message
                var submitSm = new AMQPNetLite.Smpp.PDU.Submit.SubmitSm
                {
                    SourceAddress = from,
                    DestinationAddress = toPhoneNumber,
                    DataCoding = DataCodings.UCS2,
                    ShortMessage = Encoding.BigEndianUnicode.GetBytes(message)
                };

                // Send the message
                var response = await client.Submit(submitSm);

                // Check the response
                if (response.Header.Status == CommandStatus.ESME_ROK)
                {
                    Console.WriteLine("Message sent successfully.");
                }
                else
                {
                    Console.WriteLine($"Failed to send message. Status: {response.Header.Status}");
                }
            }

            // Wait for user input to keep the console app running
            Console.WriteLine("Press Enter to exit.");
            Console.ReadLine();
        }
    }
}
