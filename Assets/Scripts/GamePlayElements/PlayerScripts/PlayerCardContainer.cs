using System.Collections.Generic;
using UnityEngine;

public class PlayerCardContainer: MonoBehaviour
{
    private List<ICard> _selectedCards = new List<ICard>();

    public IEnumerable<ICard> SelectedCards => _selectedCards;
}