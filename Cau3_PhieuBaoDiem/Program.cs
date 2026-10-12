namespace Cau3_PhieuBaoDiem;

static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main(string[] args)
    {
        if (args.Length > 0 && args[0].Equals("--test", StringComparison.OrdinalIgnoreCase))
        {
            int exitCode = VerificationTests.RunAllTests();
            Environment.Exit(exitCode);
            return;
        }

        // To customize application configuration such as set high DPI settings or default font,
        // see https://aka.ms/applicationconfiguration.
        ApplicationConfiguration.Initialize();
        Application.Run(new Form1());
    }    
}