import jwt from 'jsonwebtoken';

const publicPaths = [
  '/health',
  '/users/auth/login',
  '/users/auth/register',
  '/users/health'
];

const JWT_SECRET = process.env.JWT_SECRET || 'super-secret-change-me';

export function auth(req, res, next) {
  if (publicPaths.some(p => req.path.startsWith(p))) {
    return next();
  }

  const authHeader = req.headers['authorization'];
  if (!authHeader) return res.status(401).json({ error: 'Missing Authorization header' });

  const [scheme, token] = authHeader.split(' ');
  if (scheme !== 'Bearer' || !token) return res.status(401).json({ error: 'Invalid Authorization format' });

  try {
    const payload = jwt.verify(token, JWT_SECRET);
    
    req.user = payload;
    return next();
  } catch (err) {
    console.error(`[${req.correlationId}] JWT verify error:`, err.message);
    return res.status(401).json({ error: 'Invalid token' });
  }
}