//-----------------------------------------------------------------
//  Copyright 2013 Alex McAusland and Ballater Creations
//  All rights reserved
//  www.outlinegames.com
//-----------------------------------------------------------------
using System;
using System.Collections.Generic;
using Uniject;
using Mono.Xml;
using System.IO;

namespace Unibill.Impl {

    /// <summary>
    /// Elementary single pass XML parser that converts an element into a series of dictionaries.
    /// </summary>
    public class UnibillXmlParser : SmallXmlParser.IContentHandler {

        private SmallXmlParser parser;
        private IResourceLoader loader;
        private List<UnibillXElement> result;
        private string seeking;

        public UnibillXmlParser (SmallXmlParser parser, IResourceLoader loader) {
            this.loader = loader;
            this.parser = parser;
        }

        public List<UnibillXElement> Parse(string resourceFile, string forElements) {
            result = new List<UnibillXElement>();
            this.seeking = forElements;
            using (TextReader reader = loader.openTextFile(resourceFile)) {
                parser.Parse(reader, this);
            }

            return result;
        }

        public class UnibillXElement {
            public Dictionary<string, string> attributes { get; private set; }
            public Dictionary<string, string> kvps { get; private set; }

            public UnibillXElement(Dictionary<string, string> attributes, Dictionary<string, string> kvps) {
                this.attributes = attributes;
                this.kvps = kvps;
            }
        }

        private bool reading;
        private Dictionary<string, string> currentAttributes;
        private Dictionary<string, string> currentKvps;
        private string currentName;

        public void OnStartParsing (SmallXmlParser parser) {
        }
            
        public void OnEndParsing (SmallXmlParser parser) {
        }
            
        public void OnStartElement (string name, SmallXmlParser.IAttrList attrs) {
            if (reading) {
                currentName = name;
            } else if (name == seeking) {
                currentAttributes = new Dictionary<string, string>();
                for (int t = 0; t < attrs.Length; t++) {
                    currentAttributes[attrs.Names[t]] = attrs.Values[t];
                }
                currentKvps = new Dictionary<string, string>();
                reading = true;
            }
        }
            
        public void OnEndElement (string name) {
            if (name.Equals (seeking)) {
                reading = false;
                result.Add(new UnibillXElement(currentAttributes, currentKvps));
            }
        }

        public void OnChars (string s) {
            if (reading) {
                currentKvps [currentName] = s;
            }
        }
            
        public void OnIgnorableWhitespace (string s) {
        }
            
        public void OnProcessingInstruction (string name, string text) {
        }
    }
}
