(function () {
    window.addEventListener("load", function () {
        setTimeout(function () {

            var logo = document.getElementsByClassName('link');

            if (logo.length > 0 && logo[0].children.length > 0) {
                var logoImage = logo[0].children[0];
                logoImage.alt = "Logo";
                logoImage.src = "/images/logo/logo.jpg";

                // Adicionando CSS para ajustar tamanho e largura
                logoImage.style.width = "200px"; // ajuste o valor conforme necessário
                logoImage.style.height = "50px"; // ajuste o valor conforme necessário
            }
        }, 0); // Ajustando o tempo de delay para 0 para executar imediatamente após o carregamento
    });
})();