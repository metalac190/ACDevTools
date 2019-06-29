Implementation Notes:
1. Create a variable reference to the UI_ModalPanel prefab
2. Create a private ModalConfirmPanel -> Instantiate a new ModalConfirmPanel into it
3. ModalConfirmPanel.Initialize() and give it the info it needs
4. Create a function and add it to the ModalConfirmPanel.OnConfirmPress event
5. In your new function, when it's called remove it from the ModalConfirmPanel.OnConfirmPress function