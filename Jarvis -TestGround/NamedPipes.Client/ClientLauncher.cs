namespace NamedPipes.Client
{
    class ClientLauncher
    {
        public static void Main(string[] args)
        {
            ExampleClient example = new ExampleClient();
            example.Start(args);

            //JarvisClient jarvisClient = new JarvisClient();
            //jarvisClient.Start();
        }
    }
}
