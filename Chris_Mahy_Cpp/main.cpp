#include <iostream>
#define Q cout
using std::Q;
int main()
{
char*n[] = { "fly","spider","bird","cat","dog","cow","horse...",
"","That wriggled and wiggled and tiggled inside her;\n","How absurd to swallow a bird.\n","Fancy that to swallow a cat!\n","What a hog, to swallow a dog;\n","I don't know how she swallowed a cow;\n",
"There was an old lady who swallowed a ","I don't know why she swallowed a fly - Perhaps she'll die!","There was an old lady that swallowed a ",
"She swallowed the "," to catch the ","",",","; ","; ","; ",","};
    for (int v=0;v<6;v++)
    {
        Q<<n[13+(v==4?2:0)]<<n[v]<<n[18+v]<<"\n"<<n[v+7];
        for (int i=v; i>0;i--)
        {
            Q<<n[16]<<n[i]<<n[17]<<n[i-1]<<((i==1)?";\n":",\n");
        }
        Q<<n[14]<<"\n";
    }
    Q<<n[13]<<n[6]<<"\nShe's dead, of course!\n";
}