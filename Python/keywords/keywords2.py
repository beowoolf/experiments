import spacy
from collections import Counter
from string import punctuation
nlp = spacy.load("pl_core_news_sm")
def get_hotwords(text):
    result = []
    pos_tag = ['PROPN', 'ADJ', 'NOUN'] 
    doc = nlp(text.lower()) 
    for token in doc:
        if(token.text in nlp.Defaults.stop_words or token.text in punctuation):
            continue
        if(token.pos_ in pos_tag):
            result.append(token.text)
    return result
# new_text = """
# When it comes to evaluating the performance of keyword extractors, you can use some of the standard metrics in machine learning: accuracy, precision, recall, and F1 score. However, these metrics don’t reflect partial matches. they only consider the perfect match between an extracted segment and the correct prediction for that tag.
# Fortunately, there are some other metrics capable of capturing partial matches. An example of this is ROUGE.
# """
with open('text2.txt', encoding="utf8") as f:
    lines = f.readlines()
# print(lines)
new_text = ' '.join(lines)
output = set(get_hotwords(new_text))
most_common_list = Counter(output).most_common(10)
for item in most_common_list:
  print(item[0])
