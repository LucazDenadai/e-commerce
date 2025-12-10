import express from 'express';
import { correlationId } from './middlewares/correlationId.js';
import { createServiceProxy } from './proxy/createServiceProxy.js';
import { logger } from './middlewares/logger.js';

const app = express();
const PORT = process.env.PORT || 3000;

const USER_SERVICE_URL = process.env.USER_SERVICE_URL || 'http://localhost:5001';
const PRODUCT_SERVICE_URL = process.env.PRODUCT_SERVICE_URL || 'http://localhost:5002';

app.use(express.json());

app.use(correlationId);

// log básico de requisição/resposta
app.use(logger);

app.get('/health', (req, res) => {
  res.json({ status: 'ok', service: 'api-gateway' });
});

app.use('/users', createServiceProxy(USER_SERVICE_URL));

app.use('/products', createServiceProxy(PRODUCT_SERVICE_URL));

app.listen(PORT, () => {
  console.log(`API Gateway running on port ${PORT}`);
});