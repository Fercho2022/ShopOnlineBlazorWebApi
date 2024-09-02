function MakeUpdateQtyButtonVisible(id, visible)
{
    //Selecciona el botón HTML que tiene un atributo data-itemId igual al id proporcionado.
    //Esto supone que los botones en tu HTML están asociados con un atributo data - itemId
    //que corresponde a los identificadores de los artículos en el carrito.

    const updateQtyButton = document.querySelector("button[data-itemId='" + id + "']");

    if (visible == true) {

        updateQtyButton.style.display = "inline-block";
    }
    else {
        updateQtyButton.style.display = "none";
    }
}