// Simple Node.js mock backend for demo/testing
// Run: npm install express cors; node mock-backend.js
const express = require('express');
const cors = require('cors');
const app = express();
app.use(cors());
app.use(express.json());

app.get('/api/health', (req, res) => res.json({ status: 'ok', mock: true }));
app.post('/api/auth/register', (req, res) => res.json({ message: 'Registration successful! (mocked)' }));
app.post('/api/auth/login', (req, res) => res.json({ token: 'mock.jwt.token.1234567890' }));

const mockProjects = [
  { id: '1', name: 'Demo Project', description: 'A sample project', owner: 'demo@user.com' },
  { id: '2', name: 'Test Project', description: 'Another example', owner: 'test@user.com' }
];

app.get('/api/projects', (req, res) => res.json({ pageNumber: 1, pageSize: 10, total: mockProjects.length, items: mockProjects }));
app.get('/api/projects/:id', (req, res) => {
  const p = mockProjects.find(x => x.id === req.params.id);
  res.json(p || { id: req.params.id, name: 'Unknown', description: 'No data', owner: 'n/a' });
});
app.post('/api/projects', (req, res) => {
  const newProj = { ...req.body, id: (mockProjects.length+1).toString(), owner: 'mock@user.com' };
  mockProjects.push(newProj);
  res.json(newProj);
});
app.put('/api/projects/:id', (req, res) => {
  const idx = mockProjects.findIndex(x => x.id === req.params.id);
  if (idx >= 0) Object.assign(mockProjects[idx], req.body);
  res.status(204).send();
});
app.delete('/api/projects/:id', (req, res) => {
  const idx = mockProjects.findIndex(x => x.id === req.params.id);
  if (idx >= 0) mockProjects.splice(idx, 1);
  res.status(204).send();
});

app.listen(5000, () => console.log('Mock backend running on http://localhost:5000'));
