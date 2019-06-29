using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// This class defines the behaviour of a Tooltip box
/// Created by: Adam Chandler
/// //TODO make this work with TextMeshPro
/// </summary>
namespace ACDev.UI
{
    [RequireComponent(typeof(RectTransform))]
    public class TooltipBox : MonoBehaviour
    {
        [SerializeField] Text _tooltipTextUI = null;
        //[SerializeField] TextMeshProUGUI _toolTipTextUIPro = null;
        [SerializeField] int _maxCharacters = 100;
        [SerializeField] float _xScaleFactor = 15;
        [SerializeField] float _yScaleFactor = 3;
        [SerializeField] float _minWidth = 200;
        [SerializeField] float _maxWidth = 400;
        [SerializeField] float _minHeight = 50;
        [SerializeField] float _maxHeight = 150;

        RectTransform _rectTransform;

        public void Initialize(Transform newParent, string tooltipText, Vector3 offset)
        {
            _rectTransform = GetComponent<RectTransform>();
            // ensure that prefab transforms are zero'd out, or repositioning is odd after parenting
            _rectTransform.localPosition = new Vector3(0, 0, 0);
            transform.SetParent(newParent);

            tooltipText = ValidateText(tooltipText);
            SetText(tooltipText);

            ResizeTooltip(tooltipText);
            Reposition(offset);
        }

        string ValidateText(string tooltipText)
        {
            if (tooltipText.Length > _maxCharacters)
            {
                tooltipText = TrimOffEnd(tooltipText, _maxCharacters, true);
            }
            return tooltipText;
        }

        string TrimOffEnd(string stringToTrim, int characterLimit, bool returnWithElipses)
        {
            // if our we haven't passed the trim threshold, just return what we have
            if (stringToTrim.Length < characterLimit)
                return stringToTrim;

            // count from the end, and work towards the end to make sure we don't go over our limit
            string shortenedString = stringToTrim.Substring(0, characterLimit);
            // account for elipses
            if (returnWithElipses)
            {
                shortenedString = shortenedString + "...";
            }

            return shortenedString;
        }

        private void Reposition(Vector3 offset)
        {
            Vector3 sizeOffset = CalculateSizeOffsets(offset);
            // add current position, to designer offset, to size adjusted offset for the total
            float newXPos = transform.localPosition.x + offset.x + sizeOffset.x;
            float newYPos = transform.localPosition.y + offset.y + sizeOffset.y;

            transform.localPosition = new Vector3(newXPos, newYPos, 0);
        }

        private void ResizeTooltip(string tooltipText)
        {
            // auto scale tooltip, but constrain so it can't get infinitely big
            float xScale = tooltipText.Length * _xScaleFactor;
            float yScale = tooltipText.Length * _yScaleFactor;
            Vector2 resizedDimensions = ConstrainToSizeLimits(xScale, yScale);

            _rectTransform.sizeDelta = resizedDimensions;
        }

        Vector3 CalculateSizeOffsets(Vector3 offset)
        {
            // Use offset to determine how we're shifting, to account for size
            float xSizeOffset = 0;
            float ySizeOffset = 0;
            // calculate x
            if (offset.x > 0)
            {
                // move in positive direction, if original offset is pos
                xSizeOffset = _rectTransform.sizeDelta.x / 2;
            }
            else if (offset.x < 0)
            {
                // move in negative direction, if original offset is neg
                xSizeOffset = -_rectTransform.sizeDelta.x / 2;
            }
            // calculate y
            if (offset.y > 0)
            {
                // move in positive direction, if original offset is pos
                ySizeOffset = _rectTransform.sizeDelta.y / 2;
            }
            else if (offset.y < 0)
            {
                // move in negative direction, if original offset is neg
                ySizeOffset = -_rectTransform.sizeDelta.y / 2;
            }

            return new Vector3(xSizeOffset, ySizeOffset, 0);
        }

        Vector2 ConstrainToSizeLimits(float xScale, float yScale)
        {
            // constrain width
            if (xScale < _minWidth)
            {
                xScale = _minWidth;
            }
            if (xScale > _maxWidth)
            {
                xScale = _maxWidth;
            }
            // constrain height
            if (yScale < _minHeight)
            {
                yScale = _minHeight;
            }
            if (yScale > _maxHeight)
            {
                yScale = _maxHeight;
            }

            return new Vector2(xScale, yScale);
        }

        private void SetText(string tooltipText)
        {
            _tooltipTextUI.text = tooltipText;
            //_toolTipTextUIPro.text = tooltipText;
        }
    }
}

