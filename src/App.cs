
import React, { useState, useRef } from 'react';
import { Download, Upload, Type, Palette, Move, RotateCcw } from 'lucide-react';

function App() {
  const [image, setImage] = useState(null);
  const [texts, setTexts] = useState([]);
  const [selectedText, setSelectedText] = useState(null);
  const [showTextControls, setShowTextControls] = useState(false);
  const canvasRef = useRef(null);
  const fileInputRef = useRef(null);

  const handleImageUpload = (event) => {
    const file = event.target.files[0];
    if (file) {
      const reader = new FileReader();
      reader.onload = (e) => {
        setImage(e.target.result);
      };
      reader.readAsDataURL(file);
    }
  };

  const addText = () => {
    const newText = {
      id: Date.now(),
      text: 'Click to edit',
      x: 50,
      y: 50,
      fontSize: 24,
      color: '#A3FF00',
      fontWeight: 'bold',
      textShadow: '2px 2px 4px rgba(0,0,0,0.8)',
      outline: '1px solid #000000'
    };
    setTexts([...texts, newText]);
    setSelectedText(newText.id);
    setShowTextControls(true);
  };

  const updateText = (id, updates) => {
    setTexts(texts.map(text => 
      text.id === id ? { ...text, ...updates } : text
    ));
  };

  const downloadImage = () => {
    // This would implement canvas-based image generation
    // For now, just show a placeholder
    alert('Download feature would export your thumbnail here!');
  };

  return (
    <div className="min-h-screen bg-pixr-dark text-white font-pixel">
      {/* Header */}
      <header className="bg-pixr-gray border-b-2 border-neon-green p-4">
        <div className="flex items-center justify-between">
          <h1 className="text-xl text-neon-green">PIXR.DEV</h1>
          <div className="flex gap-2">
            <button
              onClick={() => fileInputRef.current?.click()}
              className="flex items-center gap-2 bg-neon-green text-black px-3 py-2 text-xs hover:bg-opacity-80 transition-colors"
            >
              <Upload size={14} />
              UPLOAD
            </button>
            <button
              onClick={downloadImage}
              className="flex items-center gap-2 bg-neon-pink text-black px-3 py-2 text-xs hover:bg-opacity-80 transition-colors"
            >
              <Download size={14} />
              SAVE
            </button>
          </div>
        </div>
      </header>

      <div className="flex flex-col lg:flex-row h-screen">
        {/* Canvas Area */}
        <div className="flex-1 p-4 flex items-center justify-center bg-pixr-gray">
          <div className="relative border-2 border-neon-green max-w-full max-h-full">
            {image ? (
              <div className="relative">
                <img 
                  src={image} 
                  alt="Thumbnail" 
                  className="max-w-full max-h-96 object-contain"
                />
                {texts.map(text => (
                  <div
                    key={text.id}
                    className="absolute cursor-move select-none"
                    style={{
                      left: `${text.x}px`,
                      top: `${text.y}px`,
                      fontSize: `${text.fontSize}px`,
                      color: text.color,
                      fontWeight: text.fontWeight,
                      textShadow: text.textShadow,
                      WebkitTextStroke: text.outline
                    }}
                    onClick={() => {
                      setSelectedText(text.id);
                      setShowTextControls(true);
                    }}
                  >
                    {text.text}
                  </div>
                ))}
              </div>
            ) : (
              <div className="w-96 h-64 flex items-center justify-center border-2 border-dashed border-neon-green">
                <div className="text-center">
                  <Upload className="mx-auto mb-2 text-neon-green" size={32} />
                  <p className="text-sm">Upload an image to start</p>
                </div>
              </div>
            )}
          </div>
        </div>

        {/* Sidebar */}
        <div className="w-full lg:w-80 bg-pixr-dark border-l-2 border-neon-green p-4">
          <div className="space-y-4">
            {/* Tools */}
            <div className="border border-neon-green p-3">
              <h3 className="text-sm text-neon-green mb-3">TOOLS</h3>
              <div className="grid grid-cols-2 gap-2">
                <button
                  onClick={addText}
                  className="flex items-center justify-center gap-2 bg-neon-green text-black p-2 text-xs hover:bg-opacity-80 transition-colors"
                >
                  <Type size={14} />
                  TEXT
                </button>
                <button className="flex items-center justify-center gap-2 bg-gray-600 text-white p-2 text-xs">
                  <Palette size={14} />
                  COLORS
                </button>
              </div>
            </div>

            {/* Text Controls */}
            {showTextControls && selectedText && (
              <div className="border border-neon-green p-3">
                <h3 className="text-sm text-neon-green mb-3">TEXT EDITOR</h3>
                {(() => {
                  const text = texts.find(t => t.id === selectedText);
                  if (!text) return null;
                  
                  return (
                    <div className="space-y-3">
                      <input
                        type="text"
                        value={text.text}
                        onChange={(e) => updateText(selectedText, { text: e.target.value })}
                        className="w-full bg-pixr-gray border border-neon-green p-2 text-xs text-white"
                        placeholder="Enter text..."
                      />
                      
                      <div>
                        <label className="block text-xs text-neon-green mb-1">SIZE: {text.fontSize}px</label>
                        <input
                          type="range"
                          min="12"
                          max="72"
                          value={text.fontSize}
                          onChange={(e) => updateText(selectedText, { fontSize: parseInt(e.target.value) })}
                          className="w-full"
                        />
                      </div>
                      
                      <div>
                        <label className="block text-xs text-neon-green mb-1">COLOR</label>
                        <input
                          type="color"
                          value={text.color}
                          onChange={(e) => updateText(selectedText, { color: e.target.value })}
                          className="w-full h-8 bg-pixr-gray border border-neon-green"
                        />
                      </div>
                    </div>
                  );
                })()}
              </div>
            )}

            {/* Export Settings */}
            <div className="border border-neon-green p-3">
              <h3 className="text-sm text-neon-green mb-3">EXPORT</h3>
              <div className="space-y-2">
                <button className="w-full bg-neon-green text-black p-2 text-xs hover:bg-opacity-80 transition-colors">
                  1920x1080 (YouTube)
                </button>
                <button className="w-full bg-neon-pink text-black p-2 text-xs hover:bg-opacity-80 transition-colors">
                  1080x1080 (Instagram)
                </button>
                <button className="w-full bg-neon-mint text-black p-2 text-xs hover:bg-opacity-80 transition-colors">
                  1080x1920 (TikTok)
                </button>
              </div>
            </div>
          </div>
        </div>
      </div>

      {/* Hidden file input */}
      <input
        ref={fileInputRef}
        type="file"
        accept="image/*"
        onChange={handleImageUpload}
        className="hidden"
      />
    </div>
  );
}

export default App;
