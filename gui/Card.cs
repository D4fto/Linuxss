using Godot;
using System;

public partial class Card : Control
{
	public int id = -1;
	public void setImage(int number){
		GetNode<TextureRect>("TextureButton/VBoxContainer/MarginContainer/TextureRect").Texture = (CompressedTexture2D)GD.Load("res://src/imgs/cards/" + number + ".png");;
	}
	public void setText(string text){
		GetNode<Label>("TextureButton/VBoxContainer/MarginContainer2/Label").Text = text;
	}	
	public void setRest(string max, string x){
		if(max!="-1")
		{
			GetNode<Label>("Label").Text = "Restam "+(int.Parse(max)-int.Parse(x))+".";
		}
	}	
	public void OnVBoxContainerMouseEntered(){
		float h, s, v;
		Modulate.ToHsv(out h, out s, out v);
		Modulate = Color.FromHsv(h, .1f, 1.0f);
	}
	public void OnVBoxContainerMouseExited(){
		float h, s, v;
		Modulate.ToHsv(out h, out s, out v); 
		Modulate = Color.FromHsv(h, .85f, 1.0f);
	}
	public void setType(string type)
	{
		switch(type)
		{
			case "A":
				Modulate = Color.FromHsv(188f / 360f, 0.85f, 1.0f);
				break;
			case "B":
				Modulate = Color.FromHsv(278f / 360f, 0.85f, 1.0f);
				break;
			case "C":
				Modulate = Color.FromHsv(60f / 360f, 0.85f, 1.0f);
				break;
		}
	}
	public void OnTextureButtonButtonDown()
	{
		GetParent().GetParent().GetParent<Cards>().clear();
		GetParent().GetParent().GetParent<Cards>().clicked(id);
	}
}
