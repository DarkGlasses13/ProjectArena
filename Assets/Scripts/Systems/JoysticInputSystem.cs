using Leopotam.Ecs;
using Leopotam.Ecs.Ui.Components;
using UnityEngine;

public class JoysticInputSystem : IEcsRunSystem
{
    private InputData _inputData;
    private EcsFilter<EcsUiBeginDragEvent> _beginDragFilter;
    private EcsFilter<EcsUiDragEvent> _dragFilter;
    private EcsFilter<EcsUiEndDragEvent> _endDragFilter;
    private Vector2 _startPosition;
    private const float _radius = 70;

    public void Run()
    {
        foreach (int index in _beginDragFilter)
        {
            ref EcsUiBeginDragEvent beginDragEvent = ref _beginDragFilter.Get1(index);

            if (beginDragEvent.WidgetName == UiElementsNames.Joystick)
            {
                _startPosition = beginDragEvent.Sender.GetComponent<RectTransform>().anchoredPosition;
            }
        }

        foreach (int index in _dragFilter)
        {
            ref EcsUiDragEvent dragEvent = ref _dragFilter.Get1(index);

            if (dragEvent.WidgetName == UiElementsNames.Joystick)
            {
                Vector2 joysticPosition = dragEvent.Sender.GetComponent<RectTransform>().anchoredPosition += new Vector2(dragEvent.Delta.x, _startPosition.y);

                dragEvent.Sender.GetComponent<RectTransform>().anchoredPosition = new Vector2
                (
                    Mathf.Clamp(joysticPosition.x, _startPosition.x - _radius, _startPosition.x + _radius),
                    joysticPosition.y
                );

                _inputData.JoysticDeflection = Vector2.Distance(_startPosition, joysticPosition);
                
                if (joysticPosition.x > 0)
                {
                    _inputData.JoysticSide = JoysticSide.Right;
                }

                if (joysticPosition.x < 0)
                {
                    _inputData.JoysticSide = JoysticSide.Left;
                }

                if (joysticPosition == _startPosition)
                {
                    _inputData.JoysticSide = JoysticSide.Stand;
                }
            }
        }

        foreach (int index in _endDragFilter)
        {
            ref EcsUiEndDragEvent endDragEvent = ref _endDragFilter.Get1(index);

            if (endDragEvent.WidgetName == UiElementsNames.Joystick)
            {
                endDragEvent.Sender.GetComponent<RectTransform>().anchoredPosition = _startPosition;
                _inputData.JoysticSide = JoysticSide.Stand;
            }
        }
    }
}
public enum JoysticSide
{
    Stand,
    Left,
    Right
}
