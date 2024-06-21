import express from 'express';
import bodyParser from 'body-parser';
import cors from 'cors';
import fs from 'fs';
import path from 'path';

const app = express();
const port = 3000;

const dbFilePath = path.join(__dirname, 'db.json');

app.use(bodyParser.json());
app.use(cors());

app.get('/ping', (req, res) => {
  res.send(true);
});

// Ensure db.json exists
if (!fs.existsSync(dbFilePath)) {
  fs.writeFileSync(dbFilePath, JSON.stringify([]));
}

app.post('/submit', (req, res) => {
  const { name, email, phone, github_link, stopwatch_time } = req.body;
  try {
    const data = JSON.parse(fs.readFileSync(dbFilePath, 'utf-8'));
    data.push({ name, email, phone, github_link, stopwatch_time });
    fs.writeFileSync(dbFilePath, JSON.stringify(data, null, 2));
    res.send('Submission saved successfully');
  } catch (error) {
    res.status(500).send('Error saving submission');
  }
});

app.get('/submissions', (req, res) => {
  try {
    const data = JSON.parse(fs.readFileSync(dbFilePath, 'utf-8'));
    res.json(data);
  } catch (error) {
    res.status(500).send('Error reading submissions');
  }
});

app.get('/read', (req, res) => {
  const index = parseInt(req.query.index as string, 10);
  try {
    const data = JSON.parse(fs.readFileSync(dbFilePath, 'utf-8'));
    if (index >= 0 && index < data.length) {
      res.json(data[index]);
    } else {
      res.status(404).send('Entry not found');
    }
  } catch (error) {
    res.status(500).send('Error reading entry');
  }
});

app.get('/', (req, res) => {
  res.send('Welcome to the SlidelyAI Backend API');
});

app.delete('/delete', (req, res) => {
  const index = parseInt(req.query.index as string, 10);
  try {
    const data = JSON.parse(fs.readFileSync(dbFilePath, 'utf-8'));
    if (index >= 0 && index < data.length) {
      data.splice(index, 1);
      fs.writeFileSync(dbFilePath, JSON.stringify(data, null, 2));
      res.send('Submission deleted successfully');
    } else {
      res.status(404).send('Entry not found');
    }
  } catch (error) {
    res.status(500).send('Error deleting entry');
  }
});

app.put('/edit', (req, res) => {
  const { index, name, email, phone, github_link, stopwatch_time } = req.body;
  try {
    const data = JSON.parse(fs.readFileSync(dbFilePath, 'utf-8'));
    if (index >= 0 && index < data.length) {
      data[index] = { name, email, phone, github_link, stopwatch_time };
      fs.writeFileSync(dbFilePath, JSON.stringify(data, null, 2));
      res.send('Submission updated successfully');
    } else {
      res.status(404).send('Entry not found');
    }
  } catch (error) {
    res.status(500).send('Error updating entry');
  }
});
// Add this code in index.ts
app.get('/search', (req, res) => {
  const email = req.query.email as string;
  try {
    const data = JSON.parse(fs.readFileSync(dbFilePath, 'utf-8'));
    const submission = data.find((entry: any) => entry.email === email);
    if (submission) {
      res.json(submission);
    } else {
      res.status(404).send('Submission not found');
    }
  } catch (error) {
    res.status(500).send('Error reading submissions');
  }
});

app.listen(port, () => {
  console.log(`Server is running on http://localhost:${port}`);
});
