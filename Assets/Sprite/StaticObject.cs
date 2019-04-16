using UnityEngine;
using System.Collections;

public static class StaticObject
{
	public static float nowClass = 0;
    public static int whoCharacter = 0; //哥哥1，妹妹2
	//------------------音量音效---------------------
	public static float bgmVolume = 1f;
	public static float sfxVolume = 1f;

	//-----------------平衡條 & 血量-----------------------
	public static float balanceSlider = 100f;
	public static float playerHealth = 100f;

	//--------------------技能----------------------------
	public static int G1 = 0;
	public static int B1 = 0;
	public static int G2 = 0;
	public static int B2 = 0;
	public static int G3 = 0;
	public static int B3 = 0;
	public static int G4 = 0;
	public static int B4 = 0;
	public static int G5 = 0;
	public static int G6 = 0;

	//-------------------結局-----------------------
	public static int sHE1 = 0; //離開森林
	public static int sBE1 = 0; //迷失森林	 
	public static int sBE2 = 0; //被遺忘的事
    public static int sHE2 = 0; //真實的世界 + 彩蛋
    public static int sBE3 = 0; //萬籟俱寂的等待

    //------------------圖鑑歷程圖--------------------
    public static int ad0 = 0; //序章
    public static int ad1 = 0; //第一章
    public static int ad1_flower_red = 0; //第一章紅花
    public static int ad1_flower_blue = 0; //第一章藍花
    public static int ad1_fairy_red = 0; //第一章紅精靈
    public static int ad1_fairy_blue = 0; //第一章藍精靈
    public static int ad1_HE1 = 0;
    public static int ad1_BE1 = 0;
    public static int ad2 = 0; //第二章
    public static int ad2_clock_right = 0; //第二章對懷錶
    public static int ad2_clock_false = 0; //第二章錯懷錶
    public static int ad2_BE2 = 0;
    public static int ad3 = 0; //第三章
    public static int ad3_HE2 = 0;
    public static int ad3_BE3 = 0;


    //--------------------人物解鎖------------------------
    public static int sister = 0;
	public static int bother = 0;
	public static int hikari = 0; //追光者
	public static int book = 0;

	public static int EnemyKing = 0; //框框
	public static int J = 0;
	public static int Q = 0;
	public static int K = 0;

	public static int bobby = 0;
	public static int wiki = 0;
	public static int wiko = 0;

    public static int chichi = 0;
    public static int chacha = 0;

    public static int dida = 0;
    public static int coco = 0;
    public static int dragon = 0;
    public static int Olivia = 0;
    public static int money = 0;
    public static int secretK = 0;
    public static int Grace = 0;

    //-----------------------成就---------------------------
    public static int a01 = 0; //力量的覺醒
    public static int a02 = 0; //歷史見證者
    public static int a03 = 0; //魔法師的學徒
    public static int a04 = 0; //過去，跳起來，逃跑一起來！
    public static int a05 = 0; //哎呀腳一滑
    public static int a06 = 0; //泰山喔一喔一喔
    public static int a07 = 0; //你是擅長解謎的朋友呢
    public static int a08 = 0; //平橫超平衡
    public static int a09 = 0; //不怕死的踩花人
    public static int a10 = 0; //跟著鄉民進來看熱鬧
    public static int a11 = 0; //高樓上是誰？
    public static int a12 = 0; //吐出彩虹的嘴
    public static int a13 = 0; //你亂答系？
    public static int a14 = 0; //這才不是尼斯湖
    public static int a15 = 0; //飛高高
    public static int a16 = 0; //危機解除者
    public static int a17 = 0; //終末與起點
    public static int a18 = 0; //我這有本秘技
    public static int a19 = 0; //傳說的勇者
    public static int a20 = 0; //看不見你的車尾燈
    public static int a21 = 0; //好像很會躲
    public static int a22 = 0; //閃耀的新星
    public static int a23 = 0; //下一次會更好
    public static int a24 = 0; //現在放棄的話，遊戲就結束了喔
    public static int a25 = 0; //好討厭的感覺
    public static int a26 = 0; //失衡的冒險者
    public static int a27 = 0; //給你小心心
    public static int a28 = 0; //我渴望鮮血
    public static int a29 = 0; //吃我的魔法啦
    public static int a30 = 0; //殺了一個我還有千千萬萬個我
    public static int a31 = 0; //綠綠的
    public static int a32 = 0; //招招強大
    public static int a33 = 0; //你是烏托邦鎮長嗎？

}