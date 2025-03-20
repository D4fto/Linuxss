using Godot;
using System;
using System.Collections.Generic;

public partial class Cards : Control
{
    public PackedScene cardScene = GD.Load<PackedScene>("res://gui/Card.tscn");
    public HBoxContainer cardsContainer;
    [Export]
    public Player player;
	public Dictionary<int, List<string>> cards = new Dictionary<int, List<string>>
    {
        { 1, new List<string> { "Ataque atravessa +1 inimigo.", "8", "0" } },
        { 2, new List<string> { "-15% de recarga do ataque.", "6", "0" } },
        { 3, new List<string> { "Recupera 1 vida.", "-1", "0" } },
        { 4, new List<string> { "Aumenta a vida maxima em 1.", "15", "0"  } },
        { 5, new List<string> { "Aumenta o dano em 10.", "10", "0" } },
        { 6, new List<string> { "Recupera toda a vida.", "5", "0" } },
        { 7, new List<string> { "Aumenta o alcance de coleta em 50%.", "8", "0" } },
        { 8, new List<string> { "Aumenta o ganho de xp em 100%.", "5", "0" } },
        { 9, new List<string> { "Aumenta o ganho de xp em 25%.", "12", "0" } },
        { 10, new List<string> { "Aumenta sua velocidade em 10%.", "10", "0" } },
        { 11, new List<string> { "Aumenta o tamanho do seu ataque em 50%.", "10", "0" } },
        { 12, new List<string> { "Ganhe 100 pontos.", "-1", "0" } },
        { 13, new List<string> { "A cada 20 segundos reupera 1 de vida.", "3", "0" } },
    };
	public Dictionary<string, List<int>> rarity = new Dictionary<string, List<int>>
    {
        { "A", new List<int> {1,2,3,7,9,10,11} },
        { "B", new List<int> {4,5,8} },
        { "C", new List<int> {6,13} },

    };
	public override void _Ready()
	{
        cardsContainer = GetNode<HBoxContainer>("VBoxContainer/cardsContainer");
        clear();
	}
    public void newCards(int numberOfCards)
    {
        var rng = new RandomNumberGenerator();
		Random random = new Random();
        clear();
        for (int i = 0; i < numberOfCards; i++)
        {
            string type;
            float number = rng.Randf();
            type = "A";
            if(number>.2){
                type = "A";
            }
            else if(number > .025 && rarity["B"].Count>0){
                type = "B";
            }
            else if(rarity["C"].Count>0){
                type = "C";
            }
            var index = rarity[type][random.Next(rarity[type].Count)];
            var instance = cardScene.Instantiate<Card>();
            instance.id = index;
            instance.setText(cards[index][0]);
            instance.setImage(index);
            instance.setType(type);
            instance.setRest(cards[index][1],cards[index][2]);
			cardsContainer.AddChild(instance);
        }
    } 

    public void clear()
    {
        foreach (var card in cardsContainer.GetChildren()){
            card.QueueFree();
        }
    }
    public void clicked(int id)
    {
        hide();
        Engine.TimeScale = 1;
        useCard(id);
    }
    public void show()
    {
        Visible = true;
    }
    public void hide()
    {
        Visible = false;
    }
    public void OnGuiOnGameIncreaseLevel(int level)
    {
        Engine.TimeScale = 0;
        newCards(3);
        show();
    }
    public void useCard(int id)
    {
        switch (id)
        {
            case 1:
                player.bulletPerfuration++;
                break;
            case 2:
                player.DecreaseAttackReload(.15f);
                break;
            case 3:
                player.heal();
                break;
            case 4:
                player.increaseMaxLife();
                break;
            case 5:
                GameManager.Instance.playerDamage+=10;
                break;
            case 6:
                player.LifeToMaxLife();
                break;
            case 7:
                player.MagneticDistance=(int)(player.MagneticDistance*1.5);
                break;
            case 8:
                GameManager.Instance.playerXpModifier+=1f;
                break;
            case 9:
                GameManager.Instance.playerXpModifier+=.25f;
                break;
            case 10:
                player.SpeedModifier+=.1f;
                break;
            case 11:
                player.BulletScale+=.5f;
                break;
            case 12:
                player.Points+=100;
                player.UpdatePoints();
                break;
            case 13:
                if(cards[id][2]=="0"){
                    GameManager.Instance.Regen=20;
                    cards[id][0]="A cada 10 segundos reupera 1 de vida.";
                }
                if(cards[id][2]=="1"){
                    GameManager.Instance.Regen=10;
                    cards[id][0]="A cada 5 segundos reupera 1 de vida.";
                }
                if(cards[id][2]=="2"){
                    GameManager.Instance.Regen=5;
                }
                break;
        }
        if (id!=3 && id!=12)
        {
            int x = int.Parse(cards[id][2]);
            int max = int.Parse(cards[id][1]);
            x+=1;
            if(x>=max)
            {
                foreach (KeyValuePair<string, List<int>> item in rarity)
                {  
                    if(item.Value.Contains(id)){
                        item.Value.Remove(id);
                        if(item.Key=="A" && item.Value.Count==1){
                            item.Value.Add(12);
                        }
                        return;
                    }
                }
                return;
            }
            cards[id][2] = x.ToString();

        }
    }
}
