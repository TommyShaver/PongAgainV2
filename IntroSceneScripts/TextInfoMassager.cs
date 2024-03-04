using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;
using System;

public class TextInfoMassager : MonoBehaviour
{

    public static TextInfoMassager _iTextInfoMassager { get; set; }
    public TextMeshProUGUI _messagesText;

    private int[] _stringNumber = new int[12];

    //Core logic -----------------------------------------------------
    private void Awake()
    {
        if (_iTextInfoMassager != null && _iTextInfoMassager != this)
        {
            Destroy(this);
        }
        else
        {
            _iTextInfoMassager = this;
        }
    }

    private void Start()
    {
        StartCoroutine(RandomTextTimer(30));
    }

   
    private IEnumerator RandomTextTimer(int _timer)
    {
        int i = 0;
        while (i < _timer)
        {
            if(i == 29)
            {
                TextRandomGen();
                StartCoroutine(RandomTextTimer(30));
            }
            i++;
            yield return new WaitForSeconds(1);
        }
    }

    private void TextRandomGen()
    {
        int _stringIndex = UnityEngine.Random.Range(0, _stringNumber.Length);
        int _number = 0;

        _number += _stringIndex;
        switch (_number)
        {
            case 0:
                _messagesText.text = "Follow Dark_zelda92 on twitch.tv, DO IT NOW!!!";
                break;
            case 1:
                _messagesText.text = "What did one eye say to the other? Just between you and me, something smells.";
                break;
            case 2:
                _messagesText.text = "play in full RGB by hitting, Left Alt + Left Shift + PrintScreen";
                break;
            case 3:
                _messagesText.text = "Secret tunnnnnnnnnel";
                break;
            case 4:
                _messagesText.text = "Remember licking doorknobs is illegal on other planets";
                break;
            case 5:
                _messagesText.text = "Talk about low budget flights! No food or movies, I'm outta here! I like running better!";
                break;
            case 6:
                _messagesText.text = "One does not simply play pong 1 player.";
                break;
            case 7:
                _messagesText.text = "Yare Yare Daze";
                break;
            case 8:
                _messagesText.text = "ITS A GUNDAM!!!";
                break;
            case 9:
                _messagesText.text = "·sᴉ ʇᴉ ʇɐɥʍ ʅʅǝʇ ʇˌuɐɔ I ʇnq ɓuoɹʍ sᴉ ɓuᴉɥʇǝɯos ǝʞᴉʅ ʅǝǝɟ I";
                break;
            case 10:
                _messagesText.text = "you like jazz?";
                break;
            case 11:
                _messagesText.text = "omelette du fromage";
                break;
        }
    }

    //Text function ------------------------------------------------ 
    public void SerectSongMessage()
    {
        _messagesText.text = "You found me -_-";
    }


    public void PlayerSelectorText(bool _isPlayer1)
    {
        if (!_isPlayer1)
        {
            _messagesText.text = "Play with your freinds and see who is better then the other";
        }
        else
        {
            _messagesText.text = "No one to play with have battle with our computer player";
        }
    }

    public void AISelectorText(int i)
    {
        switch (i)
        {
            case 0:
                _messagesText.text = "Easy is a great way to learn how to play!!!";
                break;
            case 1:
                _messagesText.text = "Ready for challenge I see.";
                break;
            case 2:
                _messagesText.text = "Good Luck";
                break;
           
        }
    }

}
