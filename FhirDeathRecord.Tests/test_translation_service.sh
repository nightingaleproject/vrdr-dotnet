#!/bin/bash
cd FhirDeathRecord.HTTP
dotnet run &
sleep 10

IJE="2018MA00000100000000000010Example                                           MPerson                                            Jr.       0FTHLast                                           M 1112233331048 19700424USMA07000025MAUSUS 1025B042320007 YNNYSpanish Basque      YYYNNNNNNYNNNYYDelaware                      Crow                          Laotian                       Hmong                         Kosraean                                                    Scottish                                                                                                     Example usual occupation.                  Example kind of business.                                              20180710    A                                                                                                                                                                                                                                                                                  NNN1 041920181143N                              9                                                  M     NExample Cemetery              5 Example St.                                                                                                                               Boston                      Massachusetts               02101    Suffolk                     07000                                                                                                                                                                                                                  Boston                      02101    Suffolk                     Massachusetts               United States               1 Example Street                                                                                                               Middle                                                                                                                                                                                                                                                                                                      NHome                                              Example details of injury                                                                                                                                                                                                                                 Other                         Middlesex                   017Watertown                        MA                                     Example Immediate COD                                                                                                   minutes             Example Underlying COD 1                                                                                                2 hours             Example Underlying COD 2                                                                                                6 months            Example Underlying COD 3                                                                                                15 years            Example Contributing Condition                                                                                                                                                                                                                  Last                                                   Boston                                                                                                                                    MAMassachusetts                    Boston                      Example Funeral Home                                                                                                                                                           2 Example Street                                  Boston                      MAMassachusetts               02101    042320182000Example                                           Middle                                            Doctor                                            Sr.                                                                                  100 Example St.                                   Bedford                     MAMassachusetts               01730                    Massachusetts               Massachusetts               USUnited States                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                         "

JSONOUT=$((echo 'POST /json HTTP/1.1'
echo 'Host: localhost'
echo 'Content-Type: application/ije'
echo 'Content-Length: 5000'
echo
echo "${IJE}" ) | nc localhost 8080)

JSONLEN=${#JSONOUT}

echo $JSONOUT > json.tmp

echo
cat json.tmp
echo

IJEOUT=$(curl -d "@json.tmp" -H "Content-Type: application/fhir+json" -X POST http://localhost:8080/ije)

cd ..

echo $IJE
echo
echo $JSONOUT
echo
echo $IJEOUT

if [[ $IJE == $IJEOUT ]]; then
  echo "IJE matched! Roundtrip passed!"
  exit 0
else
  echo "IJE was different! Roundtrip failed!"
  exit 1
fi
