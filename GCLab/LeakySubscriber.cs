namespace GCLab;

class LeakySubscriber : IDisposable
{
    private Publisher _publisher;

    public LeakySubscriber(Publisher publisher)
    {
        _publisher = publisher;
        _publisher.OnSomething += Handle;
    }

    private void Handle() { /* noop */ }

    public void Dispose()
    {
        if (_publisher != null)
        {
            _publisher.OnSomething -= Handle;
            _publisher = null;
        }
    }
}
