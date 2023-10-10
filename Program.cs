using System;
using System.Windows.Forms;
using Microsoft.VisualBasic.Devices;

ICracker cracker =
    args.Length > 0 && args[0] == "fake" ?
    new FakeCracker() : new Cracker();

Random rnd = new();

cracker.Init(() =>
{

    // int initialPositionX = rnd.Next(0, 500);
    // int initialPositionY = rnd.Next(0, 1080);
    cracker.MoveTo(20, 20);

    var pos = cracker.GetPosition();

    while (pos.x < 920 && pos.y < 600)
    {
        cracker.Print(pos);
        cracker.Wait(30);
        cracker.MoveTo(pos.x + 10, pos.y + 10);
        pos = cracker.GetPosition();
    }

    while (pos.x < 920)
    {
        cracker.Print(pos);
        cracker.Wait(30);
        cracker.MoveTo(pos.x + 10, pos.y);
        pos = cracker.GetPosition();
    }

    FakeClick();
    FakeWriting("Jotinha");

    bool choice = rnd.Next(0, 2) == 1;

    if (choice)
        cracker.Write("\t");
    else
    {
        while (pos.y < 635)
        {
            cracker.MoveTo(pos.x, pos.y + 10);
            cracker.Wait(30);
            pos = cracker.GetPosition();
        }

        FakeClick();
    }
    FakeWriting("JujubaSenha");

    while (pos.y < 680)
    {
        cracker.MoveTo(pos.x, pos.y + 10);
        cracker.Wait(30);
        pos = cracker.GetPosition();
    }

    cracker.MouseLeftDown();
    cracker.Wait(50);
    cracker.MouseLeftUp();


    // Final version of all program may
    // contains exit function
    cracker.Exit();
});

void FakeWriting(string text)
{
    foreach (char c in text)
    {
        cracker.Write(c.ToString());

        var delay = rnd.Next(100, 250);

        cracker.Wait(delay);
    }
}

void FakeClick()
{
    cracker.MouseLeftDown();
    cracker.Wait(50);
    cracker.MouseLeftUp();
}

public struct Ponto
{
    public int X { get; set; }
    public int Y { get; set; }

    public Ponto(int x, int y)
    {
        this.X = x;
        this.Y = y;
    }

    public Ponto((int x, int y) tup)
    {
        this.X = tup.x;
        this.Y = tup.y;
    }

    public Ponto GetDifference(Ponto p)
    {
        return new(this.X - p.X, this.Y - p.Y);
    }
}
