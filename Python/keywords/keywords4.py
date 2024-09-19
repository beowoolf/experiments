from traceback import print_list
import spacy
from collections import Counter
from string import punctuation

nlp = spacy.load("pl_core_news_lg")

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

def get_keywords(text):
    result = []
    pos_tag = ['PROPN', 'ADJ', 'NOUN'] 
    doc = nlp(text.lower().replace("\n", ""))
    for token in doc:
        if(token.text in nlp.Defaults.stop_words or token.text in punctuation):
            continue
        if(token.pos_ in pos_tag):
            result.append(token)
    my_dic = dict(zip([token for token in result],
    [token.lemma_ for token in result]))
    lemmats = {value: 0 for key, value in my_dic.items()}
    for key in my_dic:
        value = my_dic[key]
        lemmats[value] = lemmats[value] + 1

    return lemmats

def by_value(item):
    return item[1]

with open('text21.txt', encoding="utf8") as f:
    lines = f.readlines()
new_text = ' '.join(lines)
# output = set(get_hotwords(new_text))
# most_common_list = Counter(output).most_common(10)
keywords = get_keywords(new_text)
i = 1
last = 0
for k, v in sorted(keywords.items(), key=by_value, reverse=True):
    if i > 10 and last != v:
        break
    last = v
    if v > 1:
        print(i, k, '->', v)
    i = i + 1
