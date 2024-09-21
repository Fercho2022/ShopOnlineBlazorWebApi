
function initializeMercadoPago(preferenceId) {
    const mp = new MercadoPago('APP_USR-eceefff9-4fa8-4b06-8d09-9d0d14ab70e1');
    const bricksBuilder = mp.bricks();

    // Verifica que el contenedor exista antes de intentar montar el "brick"
    const walletContainer = document.getElementById('wallet_container');
    if (!walletContainer) {
        console.error("Container 'wallet_container' not found.");
        return;
    }

    bricksBuilder.create("wallet", "wallet_container", {
        initialization: {
            preferenceId: preferenceId,
        },
        customization: {
            texts: {
                valueProp: 'smart_option',
            },
        },
    }).catch(function (error) {
        console.error("Error creating wallet: ", error);
    });
}