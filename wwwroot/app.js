let deferredPrompt;

// Escuta o evento nativo do Chrome/Android para PWA
window.addEventListener('beforeinstallprompt', (e) => {
    e.preventDefault();
    deferredPrompt = e;
    
    // Força a exibição do botão flutuante no celular
    const btnContainer = document.getElementById('pwa-install-btn');
    if (btnContainer) {
        btnContainer.style.display = 'block';
    }
});

// Função chamada ao clicar no botão flutuante
function instalarPWA() {
    if (deferredPrompt) {
        deferredPrompt.prompt();
        deferredPrompt.userChoice.then((choiceResult) => {
            if (choiceResult.outcome === 'accepted') {
                console.log('ZeloApp instalado com sucesso!');
            }
            deferredPrompt = null;
            const btnContainer = document.getElementById('pwa-install-btn');
            if (btnContainer) btnContainer.style.display = 'none';
        });
    } else {
        // Caso o evento antes do prompt já tenha passado ou esteja no iOS/Safari
        alert('Para instalar o ZeloApp no seu celular:\n\n' +
              '• No Android (Chrome): Toque nos 3 pontinhos no topo e escolha "Instalar aplicativo" ou "Adicionar à tela inicial".\n\n' +
              '• No iPhone (Safari): Toque no botão Compartilhar (quadrado com seta) e escolha "Adicionar à Tela de Início".');
    }
}