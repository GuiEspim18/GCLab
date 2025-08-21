namespace GCLab;

class Program
{
    static void Main()
    {
        Console.WriteLine("=== GCLab - Versão Corrigida ===");
        Console.WriteLine($"GC Server Mode: {System.Runtime.GCSettings.IsServerGC}\n");

        var tracker = new IssueTracker();

        // 1) Vazamento por evento resolvido
        var publisher = new Publisher();
        using (var subscriber = new LeakySubscriber(publisher))
        {
            tracker.Track("subscriber", subscriber);

            // 2) LOH + cache com WeakReference
            var lohBuffer = BigBufferHolder.Run();
            tracker.Track("lohBuffer", lohBuffer);

            // 3) Pinned buffer liberado
            using (var pinner = new Pinner())
            {
                var pinned = pinner.PinLongTime();
                tracker.Track("pinnedBuffer", pinned);
            }

            // 4) Concatenação eficiente
            var payload = ConcatWork.Fixed();
            Console.WriteLine($"Payload length: {payload.Length}");

            // 5) Logger com Dispose
            using (var logger = new Logger("log.txt"))
            {
                logger.WriteLines(10);
                tracker.Track("logger", logger);
            }

            // Dispara evento
            publisher.Raise();
        }

        // Remove referências locais
        publisher = null;

        // Coleta completa
        GCHelpers.FullCollect();
        tracker.Report();

        Console.WriteLine(tracker.HasSurvivors
            ? "\n❌ Existem sobreviventes indesejados."
            : "\n✅ GC limpo: nenhuma referência indesejada permaneceu viva.");
    }
}
