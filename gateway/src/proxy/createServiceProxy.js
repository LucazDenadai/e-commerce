import { createProxyMiddleware } from 'http-proxy-middleware';

export function createServiceProxy(target) {
    return createProxyMiddleware({
        target,
        changeOrigin: true,
        onProxyReq: (proxyReq, req) => {
            // Propaga correlation-id para os serviços internos
            if (req.correlationId) {
                proxyReq.setHeader('x-correlation-id', req.correlationId);
            }
        },
        onError: (err, req, res) => {
            console.error(`[${req.correlationId}] Proxy error:`, err.message);
            res.status(502).json({ error: 'Upstream service unavailable' });
        },
    });
}