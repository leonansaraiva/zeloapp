self.addEventListener('install', event => {
    console.log('[ZeloApp] Service Worker instalado com sucesso.');
    self.skipWaiting();
});

self.addEventListener('activate', event => {
    console.log('[ZeloApp] Service Worker ativo.');
});

self.addEventListener('fetch', event => {
    // Permite que o Blazor processe as requisições de rede normalmente
    return;
});