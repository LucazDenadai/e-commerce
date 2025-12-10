import { v4 as uuid } from 'uuid';

export function correlationId(req, res, next) {
    const correlationId = req.headers['x-correlation-id'] || uuid();
    req.correlationId = correlationId;
    res.setHeader('X-Correlation-ID', correlationId);
    next();
};