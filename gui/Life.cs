using Godot;

public partial class Life : Label
{
	public void OnGuiOnGameLifeChange(int life, int Maxlife)
	{
		Text = "";
		for (int i = 0; i<life; i++){
			Text += "❤️";
 		}
	}

}
